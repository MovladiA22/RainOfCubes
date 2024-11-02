using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _repeatRate;

    private ObjectPool<GameObject> _pool;
    private int _poolCapacity = 10;
    private int _maxSize = 10;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>
            (createFunc: () => Spawn(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: _poolCapacity,
            maxSize: _maxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    public void ReleaseCube(GameObject gameObject)
    {
        _pool.Release(gameObject);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private GameObject Spawn()
    {
        var copyPosition = GetRandomPosition();
        var copyRotation = Quaternion.identity;
        GameObject cubeCopy = Instantiate(_cubePrefab, copyPosition, copyRotation);
        
        return cubeCopy;
    }

    private void ActionOnGet(GameObject obj)
    {
        obj.transform.position = GetRandomPosition();
        obj.SetActive(true);
    }

    private Vector3 GetRandomPosition()
    {
        float minValue = -5f;
        float maxValue = 6f;
        float positionY = 15f;
        float randomPosition = Random.Range(minValue, maxValue + 1f);

        return new Vector3(randomPosition, positionY, randomPosition);
    }
}
