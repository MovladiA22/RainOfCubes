using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    public event System.Action<Cube> Collided;

    private bool _isCollided = false;
    private float _lifeTime;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollided == false && collision.gameObject.TryGetComponent<Platform>(out Platform platform))
        {
            ProcessCollision();
            Invoke(nameof(InvokeEvent), _lifeTime);
        }
    }

    public void ReturnSettings()
    {
        _renderer.material.color = Color.clear;
        _isCollided = false;
    }    

    private void InvokeEvent()
    {
        Collided?.Invoke(this);
    }

    private void ProcessCollision()
    {
        float minValue = 2;
        float maxValue = 5;

        _lifeTime = Random.Range(minValue, maxValue + 1);
        _renderer.material.color = Random.ColorHSV();
        _isCollided = true;
    }
}
