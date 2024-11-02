using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubeStandart;
    [SerializeField] private float _repeatRate;

    private ObjectPool<Cube> _pool;
    private int _poolCapacity = 10;
    private int _maxSize = 10;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>
            (createFunc: () => Spawn(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: _poolCapacity,
            maxSize: _maxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    public void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private Cube Spawn()
    {
        var copyPosition = GetRandomPosition();
        var copyRotation = Quaternion.identity;
        Cube cubeCopy = Instantiate(_cubeStandart, copyPosition, copyRotation);
        cubeCopy.Collided += ReleaseCube;

        return cubeCopy;
    }

    private void ActionOnGet(Cube obj)
    {
        obj.transform.position = GetRandomPosition();
        obj.ReturnSettings();
        obj.gameObject.SetActive(true);
        obj.Collided += ReleaseCube;
    }

    private void ActionOnRelease(Cube obj)
    {
        obj.Collided -= ReleaseCube;
        obj.gameObject.SetActive(false);
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
