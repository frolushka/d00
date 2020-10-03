using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour, ICollisionHandler
{
    public bool GameOver { get; private set; }
    
    [HideInInspector]
    public float points;
    
    [SerializeField] private float gravity;
    [SerializeField] private float punchPower;
    [Header("Physics")] 
    [SerializeField] private PhysicsObject2D_42 physics;

    private float _currentVerticalVelocity;

    private void Update()
    {
        if (GameOver)
            return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentVerticalVelocity = punchPower;
        }

        physics.Move(Time.deltaTime * _currentVerticalVelocity * Vector3.up);
        _currentVerticalVelocity -= Time.deltaTime * gravity;
    }

    public void OnCollision(Physics2D_42.CollisionData collisionData)
    {
        if (GameOver)
            return;

        GameOver = true;
        Physics2D_42.Instance.active = false;
        Debug.Log($"Score: {Mathf.RoundToInt(points)}\nTime: {Mathf.RoundToInt(Time.timeSinceLevelLoad)}s");
    }
}
