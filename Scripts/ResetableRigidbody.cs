using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ResetableRigidbody : MonoBehaviour, IResetableComponent
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ResetComponentState()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}