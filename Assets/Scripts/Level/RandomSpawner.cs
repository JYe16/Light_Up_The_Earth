using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] valuelessPrefabList;
    public GameObject[] valuablePrefabList;
    public GameObject[] propsPrefabList;
    public Transform platformTransform;
    public int level = 0;
    [System.Serializable]
    public class SpawnerData
    {
        public float innerRadius = 0.0f;
        public float outerRadius = 0.0f;
        public float collisionCheckRadius = 0.0f;
        public int valuableSum = 0;
        public int valuelessSum = 0;
        public int propsSum = 0;
    }
    [System.Serializable]
    public class SpawnerOriginJson
    {
        public SpawnerData[] spawnerDataList;
    }

    private SpawnerData spawnerData;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
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
            Vector3 randomPos = RingAreaPos(spawnerData.innerRadius, spawnerData.outerRadius, transform.position, objHeight);
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
        } while (position.y - objHeight < platformTransform.position.y + 25.0f && !Physics.CheckSphere(position, spawnerData.collisionCheckRadius));
        return position;
    }

    private void Init()
    {
        // TODO: auto update level
        spawnerData = new SpawnerData();
        string json = Utils.ReadDataFromFile("SpawnerData.json");
        spawnerData = JsonUtility.FromJson<SpawnerOriginJson>(json).spawnerDataList[level];
    }
}
