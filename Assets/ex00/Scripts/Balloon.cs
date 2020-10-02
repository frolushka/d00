using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private float defaultSize;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] private float deflateSpeed;
    [Space]
    [SerializeField] private float defaultBreath;
    [SerializeField] private float breathDecreaseValue;
    [SerializeField] private float breathIncreaseSpeed;
    [SerializeField] private float inflateValue;
    [SerializeField] private float breathDelay;

    private float _size;
    private float Size
    {
        get => _size;
        set
        {
            _size = value;
            transform.localScale = _size * Vector3.one;
            if (_size >= maxSize || _size <= minSize)
                CancelGame();
        }
    }

    private float _nextTimeAvaliableInflate;
    private float _breath;
    private float Breath
    {
        get => _breath;
        set
        {
            _breath = value > 0 ? value : 0;
            if (_breath == 0)
            {
                _nextTimeAvaliableInflate = Time.time + breathDelay;
            }
        }
    }
    public bool InflateAvailable => Time.time >= _nextTimeAvaliableInflate;
    
    private void Start()
    {
        Size = defaultSize;
        Breath = defaultBreath;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && InflateAvailable)
        {
            Size += inflateValue;
            Breath -= breathDecreaseValue;
        }
        else
        {
            Size -= deflateSpeed * Time.deltaTime;
            Breath += breathIncreaseSpeed * Time.deltaTime;
        }
    }

    private void CancelGame()
    {
        Debug.Log($"Balloon lifetime: {Mathf.RoundToInt(Time.time)}s");
        Destroy(gameObject);
    }
}
