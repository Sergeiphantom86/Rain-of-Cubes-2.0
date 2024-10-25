using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(SpawnerInfo))]

public class Spawner<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] private T _objectToSpawn;

    protected ObjectPool<T> _pool;
    private SpawnerInfo _spawnerInfo;

    protected virtual void Awake()
    {
        _spawnerInfo = GetComponent<SpawnerInfo>();

        _pool = new ObjectPool<T>(
            createFunc: CreateNew,
            actionOnGet: ActionOnGet,
            actionOnRelease: item => item.gameObject.SetActive(false),
            actionOnDestroy: DestroyObject);
    }

    protected T Spawn()
    {
        T item = _pool.Get();

        AssignValueQuantityObjects(_spawnerInfo);

        _spawnerInfo.NotifyObjectSpawned(item.gameObject);
        
        return item;
    }

    private void AssignValueQuantityObjects(SpawnerInfo spawnerInfo)
    {
        spawnerInfo.QuantityObjectsScene = _pool.CountActive;
        spawnerInfo.QuantityObjectsCreated = _pool.CountAll;
    }

    protected virtual void ActionOnGet(T item)
    {
        item.ResetInternalState();
        item.gameObject.SetActive(true);
    }

    private T CreateNew()
    {
        T newItem = Instantiate(_objectToSpawn);
        newItem.Deactivated += OnObjectDeactivated;

        _spawnerInfo.QuantityObjectsAllTime++;

        return newItem;
    }

    private void DestroyObject(T item)
    {
        item.Deactivated -= OnObjectDeactivated;

        Destroy(item.gameObject);
    }

    private void OnObjectDeactivated(PoolableObject item)
    {
        _pool.Release(item as T);

        if (item != null)
        {
            _spawnerInfo.NotifyObjectDespawned(item.gameObject);
        }
    }
}