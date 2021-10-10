using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] valuelessPrefabList;
    public GameObject[] valuablePrefabList;
    public GameObject[] propsPrefabList;
    public Transform platformTransform;

    [System.Serializable]
    public class SpawnerData
    {
        public float innerRadius;
        public float outerRadius;
        public float collisionCheckRadius;
        public int valuableSum;
        public int valuelessSum;
        public int propsSum;
		public int targetScore;
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
            float objHeight = prefab.GetComponent<MeshRenderer>().bounds.size.y / 2;
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
        // TODO: auto update level
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
		for(int i = 0; i < 100; i++)
		{
			SpawnerData spawner = new SpawnerData();
        	spawner.valuelessSum = 30;
       		spawner.valuableSum = 5;
        	spawner.propsSum = 10;
        	spawner.collisionCheckRadius = 5.0f;
        	spawner.outerRadius = 250.0f;
        	spawner.innerRadius = 170.0f;
        	spawnData.list.Add(spawner);	
		}
        string content = JsonUtility.ToJson(spawnData);
        Utils.WriteJSON("SpawnerData.json", content);
    }

}