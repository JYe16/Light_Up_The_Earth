using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] valuelessPrefabList;
    public GameObject[] valuablePrefabList;
    public GameObject[] propsPrefabList;
    [HideInInspector]public Transform platformTransform;

    [System.Serializable]
    public class SpawnerData
    {
		//物体生成内半径
        public float innerRadius;
		//物体生成外半径
        public float outerRadius;
		//防止物体重叠
        public float collisionCheckRadius;
		//有价值物体总数
        public int valuableSum;
        public int valuelessSum;
		//道具总数
        public int propsSum;
    }

    [System.Serializable]
    public class SpawnerOriginJson
    {
        public List<SpawnerData> list = new List<SpawnerData>();
    }

    private SpawnerData spawnerData;

    // Start is called before the first frame update
    void Start()
    {
		int level = 1;
		if (PlayerPrefs.HasKey("level"))
		{
			level = PlayerPrefs.GetInt("level");
		}
        Init(level - 1);
        AddObject(spawnerData.valuelessSum, valuelessPrefabList);
        AddObject(spawnerData.valuableSum, valuablePrefabList);
        AddObject(spawnerData.propsSum, propsPrefabList);
    }

    private void AddObject(int count, GameObject[] prefabs)
    {
        for (int i = 0; i < count; i++)
        {
	        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
	        GameObject originGoal = prefab;
	        if (prefab.transform.childCount > 1)
	        {
		        originGoal = prefab.transform.GetChild(0).gameObject;
	        }
	        float objHeight = originGoal.GetComponent<MeshRenderer>().bounds.size.y / 2;
            Vector3 randomPos = RingAreaPos(spawnerData.innerRadius, spawnerData.outerRadius, transform.position,
                objHeight);
            Instantiate(prefab, randomPos, Quaternion.identity);
        }
    }

    // spawn in a ring area
    public Vector3 RingAreaPos(float innerRadius, float outerRadius, Vector3 centerPos, float objHeight)
    {
        Vector3 position;
        do
        {
            position = Random.insideUnitSphere * outerRadius + centerPos;
            position = position.normalized * (innerRadius + position.magnitude);
        } while (position.y - objHeight < platformTransform.position.y + 25.0f &&
                 !Physics.CheckSphere(position, spawnerData.collisionCheckRadius));
        return position;
    }

    private void Init(int level)
    {
        spawnerData = new SpawnerData();
        //read file from device first
        string json = Utils.ReadDataFromFile("SpawnerData.json");
        //if the file hasn't been created
        if (json == "")
        {
            generateSpawnData();
            json = Utils.ReadDataFromFile("SpawnerData.json");
        }
        spawnerData = JsonUtility.FromJson<SpawnerOriginJson>(json).list[level];
    }

    //TODO: Write a method to generate level difficulty automatically
    public void generateSpawnData()
    {
		SpawnerOriginJson spawnData = new SpawnerOriginJson();
		for(int i = 0; i < 10; i++)
		{
			SpawnerData spawner = new SpawnerData();
        	spawner.valuelessSum = 30;
       		spawner.valuableSum = 8;
        	spawner.propsSum = 10;
        	spawner.collisionCheckRadius = 100.0f;
        	spawner.outerRadius = 550.0f;
        	spawner.innerRadius = 450.0f;
        	spawnData.list.Add(spawner);	
		}
        string content = JsonUtility.ToJson(spawnData);
        Utils.WriteJSON("SpawnerData.json", content);
    }
}