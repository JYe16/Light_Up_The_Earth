using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] valuelessPrefabList;

    public GameObject[] valuablePrefabList;

    public float innerRadius;
    public float outerRadius;
    public int valuableSum;
    public int valuelessSum;
    public float collisionCheckRadius;
    public Transform platformTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        AddObject(valuelessSum, valuelessPrefabList);
        AddObject(valuableSum, valuablePrefabList);
    }

    private void AddObject(int count, GameObject[] prefabs)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
            float objHeight = prefab.GetComponent<MeshRenderer>().bounds.size.y / 2;
            Vector3 randomPos = RingAreaPos(innerRadius, outerRadius, transform.position, objHeight);
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
        } while (position.y - objHeight < platformTransform.position.y + 5.0f && !Physics.CheckSphere(position, collisionCheckRadius));
        return position;
    }
}
