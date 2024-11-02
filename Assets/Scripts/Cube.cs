using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] CubeSpawner _cubeSpawner;

    private bool _isCollided = false;
    private float _lifeTime;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollided == false)
        {
            ProcessCollision();

            Invoke(nameof(Release), _lifeTime);
        }
    }
    
    private void Release() =>
        _cubeSpawner.ReleaseCube(gameObject);

    private void ProcessCollision()
    {
        float minValue = 2;
        float maxValue = 5;

        _lifeTime = Random.Range(minValue, maxValue + 1);
        _renderer.material.color = Random.ColorHSV();
        _isCollided = true;
    }
}
