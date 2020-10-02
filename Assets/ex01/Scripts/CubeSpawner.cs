using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    [SerializeField] private float minSpawnDelay;
    [SerializeField] private float maxSpawnDelay;

    [Space] 
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private GameObject prefab;

    private GameObject _currentCube;
    private GameObject CurrentCube
    {
        get => _currentCube;
        set
        {
            if (_currentCube)
            {
                Destroy(_currentCube);
            }
            _currentCube = value;
        }
    }
    private float _nextSpawn;
    
    private void Start()
    {
        _nextSpawn = Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    private void Update()
    {
        if (Time.time >= _nextSpawn)
        {
            CurrentCube = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            _nextSpawn = Time.time + Random.Range(minSpawnDelay, maxSpawnDelay);
        }

        if (Input.GetKeyDown(key) && _currentCube)
        {
            Debug.Log($"Precision: {Vector3.Distance(targetPoint.position, _currentCube.transform.position)}");
            CurrentCube = null;
        }
    }
}
