using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(RandomColorAssigner))]

public class FallingCube : PoolableObject, IResetableComponent
{
    [SerializeField, Min(0f)] private float _minDelay;
    [SerializeField, Min(0f)] private float _maxDelay;

    private bool _hasCollided;
    private Coroutine _coroutine;
    private RandomColorAssigner randomColorAssigner;

    private void OnValidate()
    {
        _maxDelay = Mathf.Max(_minDelay, _maxDelay);
    }

    private void Awake()
    {
        randomColorAssigner = GetComponent<RandomColorAssigner>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform _) && _hasCollided == false)
        {
            _hasCollided = true;
            randomColorAssigner.Replace();

            _coroutine = StartCoroutine(WaitAndDeactivate());
        }
    }

    public void ResetComponentState()
    {
        _hasCollided = false;
        randomColorAssigner.Default();
        transform.rotation = new Quaternion();

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator WaitAndDeactivate()
    {
        yield return new WaitForSeconds(Random.Range(_minDelay, _maxDelay));

        Deactivate(this);
    }
}