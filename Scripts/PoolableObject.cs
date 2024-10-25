using System;
using UnityEngine;

[DisallowMultipleComponent]

public class PoolableObject : MonoBehaviour
{
    public event Action<PoolableObject> Deactivated;

    public void Deactivate(PoolableObject item)
    {
        if (item is Bomb _)
        {
            gameObject.SetActive(false);
            return;
        }

        Deactivated?.Invoke(this);
    }

    public void ResetInternalState()
    {
        foreach (IResetableComponent resetableComponent in GetComponents<IResetableComponent>())
            resetableComponent.ResetComponentState();
    }
}