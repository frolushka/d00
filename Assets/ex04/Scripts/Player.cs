using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private KeyCode keyUp;
    [SerializeField] private KeyCode keyDown;

    [SerializeField] private float speed;
    
    [Header("Physics")] 
    [SerializeField] private PhysicsObject2D_42 physics;
    
    private void Update()
    {
        if (Input.GetKey(keyUp))
        {
            physics.Move(Time.deltaTime * speed * Vector3.up);
        }
        else if (Input.GetKey(keyDown))
        {
            physics.Move(Time.deltaTime * speed * Vector3.down);
        }
    }
}
