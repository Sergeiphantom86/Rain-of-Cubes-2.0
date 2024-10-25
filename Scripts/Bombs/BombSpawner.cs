using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private SpawnerInfo _bombContainer;

    private void OnEnable()
    {
        _bombContainer.ObjectDespawned += OnBombContainerDeactivated;
    }

    private void OnDisable()
    {
        _bombContainer.ObjectDespawned -= OnBombContainerDeactivated;
    }

    private void OnBombContainerDeactivated(GameObject bombContainer)
    {
        Bomb bomb = Spawn();
        bomb.transform.SetPositionAndRotation(bombContainer.transform.position, bombContainer.transform.rotation);
        bomb.Explode();
    }

    protected override void ActionOnGet(Bomb bomb)
    {
        base.ActionOnGet(bomb);
    }
}