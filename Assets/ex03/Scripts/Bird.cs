using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour, ICollisionHandler
{
    public static bool GameOver = false;
    
    [HideInInspector]
    public float points;
    
    [SerializeField] private float gravity;
    [SerializeField] private float punchPower;

    private float _currentVerticalVelocity;

    private void Update()
    {
        if (GameOver)
            return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentVerticalVelocity = punchPower;
        }

        transform.position += Time.deltaTime * _currentVerticalVelocity * Vector3.up;
        _currentVerticalVelocity -= Time.deltaTime * gravity;
    }

    public void OnCollision(Physics2D_42.CollisionData collisionData)
    {
        if (GameOver)
            return;

        GameOver = true;
        Physics2D_42.Instance.Active = false;
        Debug.Log($"Score: {Mathf.RoundToInt(points)}\nTime: {Mathf.RoundToInt(Time.time)}s");
    }
}
