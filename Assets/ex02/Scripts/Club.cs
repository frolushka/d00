using UnityEngine;

public class Club : MonoBehaviour
{
    [SerializeField] private SpriteRenderer clubSpriteRenderer;
    [SerializeField] private Ball ball;
    [Space]
    [SerializeField] private float rotationSpeed;
    [Header("Punch settings")]
    [SerializeField] private float maxPower;
    [SerializeField] private float maxPowerTime;
    [Header("Club visual settings")]
    [SerializeField] private float defaultDistance;
    [SerializeField] private float maxDistance;

    private bool _active;
    private float _angle;

    private float _currentPower;

    private void Start()
    {
        Show(true);
    }

    private void Update()
    {
        if (!_active)
            return;
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _angle += rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _angle -= rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _currentPower += maxPower * Time.deltaTime / maxPowerTime;
            if (_currentPower >= maxPower)
                _currentPower = maxPower;
        }
        
        UpdatePosition();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ball.Punch(_currentPower * new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle)));
            Show(false);
            _currentPower = 0;
        }
    }

    public void Show(bool isVisible)
    {
        _active = isVisible;
        clubSpriteRenderer.enabled = isVisible;
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        transform.position = ball.transform.position - (defaultDistance + (maxDistance - defaultDistance) * _currentPower / maxPower) *  new Vector3(Mathf.Sin(_angle), Mathf.Cos(_angle));
    }
}
