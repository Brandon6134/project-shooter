using UnityEngine;

public abstract class PoolManagerSchema : MonoBehaviour
{
    //this pool manager schema class exists so pool objects can directly reference the pool parent object
    //(instead of each object having to find the parent pool component)
    public Pool pool;
}
