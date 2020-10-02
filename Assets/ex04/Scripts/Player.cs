using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private KeyCode keyUp;
    [SerializeField] private KeyCode keyDown;

    [SerializeField] private float speed;
    
    private void Update()
    {
        if (Input.GetKey(keyUp))
        {
            transform.Translate(Time.deltaTime * speed * Vector3.up);
        }
        else if (Input.GetKey(keyDown))
        {
            transform.Translate(Time.deltaTime * speed * Vector3.down);
        }
    }
}
