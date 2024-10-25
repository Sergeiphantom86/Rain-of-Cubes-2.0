using System;
using UnityEngine;

public class SpawnerInfo : MonoBehaviour
{
    public event Action<GameObject> ObjectSpawned;
    public event Action<GameObject> ObjectDespawned;

    public int QuantityObjectsAllTime { get; set; }
    public int QuantityObjectsCreated { get; set; }
    public int QuantityObjectsScene { get; set; }

    public void NotifyObjectSpawned(GameObject item)
    {
        ObjectSpawned?.Invoke(item);
    }

    public void NotifyObjectDespawned(GameObject item)
    {
        ObjectDespawned?.Invoke(item);
    }
}