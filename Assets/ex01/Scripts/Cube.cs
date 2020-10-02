using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private float _speed;
    
    private void Start()
    {
        _speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.down);
    }
}
