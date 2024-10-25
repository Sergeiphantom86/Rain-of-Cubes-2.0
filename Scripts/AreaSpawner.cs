using System.Collections;
using UnityEngine;

public class AreaSpawner : Spawner<PoolableObject>
{
    [SerializeField] private float _interval;

    private Coroutine _spawnCoroutine;

    protected override void Awake()
    {
        base.Awake();

        transform.position = UsePositionOnGet();
    }

    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnRepeatedly());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnCoroutine);
    }

    protected override void ActionOnGet(PoolableObject item)
    {
        SetRandomPosition(item);
        base.ActionOnGet(item);
    }

    private void SetRandomPosition(PoolableObject item)
    {
        item.transform.SetPositionAndRotation(UsePositionOnGet(), Random.rotation);
    }

    private Vector3 UsePositionOnGet()
    {
        return new Vector3(GetRandomPoint(), transform.position.y, GetRandomPoint());
    }

    private float GetRandomPoint(float minPosition = 5, float maxPosition = 35)
    {
        return Random.Range(minPosition, maxPosition);
    }

    private IEnumerator SpawnRepeatedly()
    {
        WaitForSeconds delay = new(_interval);

        while (enabled)
        {
            Spawn();
            yield return delay;
        }
    }
}