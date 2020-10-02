using UnityEngine;

public class PongBall : MonoBehaviour, ICollisionHandler
{
    private const float Offset = 0.5f;

    [SerializeField] private float speed;
    [SerializeField] private Transform playerLeft;
    [SerializeField] private Transform playerRight;
    [SerializeField] private Transform spawnPoint;

    private int _playerLeftScore;
    private int _playerRightScore;

    private Vector2 _currentVelocity;

    private void Start()
    {
        transform.position = spawnPoint.position;
        UpdateCurrentVelocity();
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * _currentVelocity);
        if (transform.position.x <= playerLeft.position.x - Offset)
        {
            AddPointAndRestart(ref _playerRightScore);
        }
        else if (transform.position.x >= playerRight.position.x + Offset)
        {
            AddPointAndRestart(ref _playerLeftScore);
        }
    }

    private void AddPointAndRestart(ref int score)
    {
        score++;
        Debug.Log($"Player 1: {_playerLeftScore} | Player 2: {_playerRightScore}");
        transform.position = spawnPoint.position;
        UpdateCurrentVelocity();
    }

    private void UpdateCurrentVelocity()
    {
        _currentVelocity = new Vector2(Sign(Random.Range(-1, 1)), Sign(Random.Range(-1, 1)));
        int Sign(int a) => a > 0 ? 1 : -1;
    }
    
    public void OnCollision(Physics2D_42.CollisionData collisionData)
    {
        if (Abs(collisionData.normal.x) > Abs(collisionData.normal.y))
        {
            _currentVelocity.x *= -1;
        }
        else
        {
            _currentVelocity.y *= -1;
        }
        
        float Abs(float a) => a >= 0 ? a : -a;
    }
}
