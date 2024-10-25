using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Explosion), typeof(Renderer))]

public class Bomb : PoolableObject, IResetableComponent
{
    [SerializeField, Min(0f)] private float _minDelay;
    [SerializeField, Min(0f)] private float _maxDelay;

    private Coroutine _explosionCoroutine;
    private Explosion _explosion;
    private Renderer _renderer;

    private void OnValidate()
    {
        _maxDelay = Mathf.Max(_minDelay, _maxDelay);
    }

    private void Awake()
    {
        _explosion = GetComponent<Explosion>();
        _renderer = GetComponent<Renderer>();
    }

    public void Explode()
    {
        TryStopCoroutine();

        _explosionCoroutine = StartCoroutine(WaitAndExplode());
    }

    public void ResetComponentState()
    {
        SetAlpha();

        TryStopCoroutine();
    }

    private void TryStopCoroutine()
    {
        if (_explosionCoroutine != null)
        {
            StopCoroutine(_explosionCoroutine);
        }
    }

    private void SetAlpha(float alpha = 1)
    {
        Color color = _renderer.material.color;
        color.a = alpha;
        _renderer.material.color = color;
    }

    private IEnumerator WaitAndExplode()
    {
        float explosionDelay = Random.Range(_minDelay, _maxDelay);
        float timeRemaining = explosionDelay;

        while (_renderer.material.color.a > 0)
        {
            SetAlpha(timeRemaining / explosionDelay);
            yield return null;
            timeRemaining -= Time.deltaTime;
        }

        _explosion.Explode();
        Deactivate(this);
    }
}