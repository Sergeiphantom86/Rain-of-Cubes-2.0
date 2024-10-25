using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _force;
    [SerializeField, Min(0f)] private float _radius;

    public void Explode()
    {
        Collider[] affectedColliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider collider in affectedColliders)
        {
            Rigidbody rigidbody = collider.attachedRigidbody;

            if (rigidbody != null)
                rigidbody.AddExplosionForce(_force, transform.position, _radius);
        }
    }
}