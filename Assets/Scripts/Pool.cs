using UnityEngine;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{
    private Queue<GameObject> pool = new Queue<GameObject>();
    public GameObject prefab;
    public int poolSize = 20;

    void Awake()
    {
        for(int i=0;i<poolSize;i++) //initialize pool with preset number of inactive prefab gameobjects
        {
            GameObject prefabObject = Instantiate(prefab,this.transform); //instantiate prefabs with this gameobject as the parent
            prefabObject.SetActive(false);
            pool.Enqueue(prefabObject);
        }
    }

    public GameObject Spawn(Vector3 spawnPos)
    {
        GameObject prefabObject = pool.Dequeue();
        prefabObject.SetActive(true);
        prefabObject.transform.position = spawnPos;
        return prefabObject;
    }

    public void Despawn(GameObject prefabObject)
    {
        prefabObject.SetActive(false);
        pool.Enqueue(prefabObject);
    }

}
