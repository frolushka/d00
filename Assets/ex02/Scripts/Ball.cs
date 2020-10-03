using UnityEngine;

public class Ball : MonoBehaviour, ICollisionHandler
{
    private const int Reward = 5;
    
    [SerializeField] private float deccelerationTime;
    [SerializeField] private SpriteRenderer ballSpriteRenderer;
    [SerializeField] private Club club;
    [Header("Hole collision")]
    [SerializeField] private float maxVelocityToWin;
    [SerializeField] private PhysicsObject2D_42 ball;
    [SerializeField] private PhysicsObject2D_42 hole;
    [Header("Physics")] 
    [SerializeField] private PhysicsObject2D_42 physics;

    private int _score = -15;
    private bool _isMoving;
    private Vector2 _startVelocity;
    private Vector2 _currentVelocity;
    
    private void Update()
    {
        if (_isMoving)
        {
            physics.Move(Time.deltaTime * _currentVelocity);
            _currentVelocity -= 1f / deccelerationTime * Time.deltaTime * _startVelocity;
            if (_currentVelocity.sqrMagnitude < 0.05f)
            {
                _currentVelocity = Vector2.zero;
                _isMoving = false;
                _score += Reward;
                Debug.Log($"Score: {_score}");
                club.Show(true);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isMoving 
            && _currentVelocity.sqrMagnitude <= maxVelocityToWin * maxVelocityToWin
            && Physics2D_42.TryIntersectBoxColliders(ball, hole, out _))
        {
            _isMoving = false;
            ballSpriteRenderer.enabled = false;
            Debug.Log($"Final score: {_score}");
        }
    }

    public void Punch(Vector2 force)
    {
        _currentVelocity = force;
        _startVelocity = force;
        _isMoving = true;
    }
    
    public void OnCollision(Physics2D_42.CollisionData collisionData)
    {
        if (Abs(collisionData.normal.x) > Abs(collisionData.normal.y))
        {
            _currentVelocity.x *= -1;
            _startVelocity.x *= -1;
        }
        else
        {
            _currentVelocity.y *= -1;
            _startVelocity.y *= -1;
        }
        
        float Abs(float a) => a >= 0 ? a : -a;
    }
}
