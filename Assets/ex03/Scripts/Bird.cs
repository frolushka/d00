using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private float punchPower;

    private float _currentVerticalVelocity;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentVerticalVelocity = punchPower;
        }

        transform.position += Time.deltaTime * _currentVerticalVelocity * Vector3.up;
        _currentVerticalVelocity -= Time.deltaTime * gravity;
    }

    private void OnBecameInvisible()
    {
        Debug.Log($"Score: TODO\nTime: {Mathf.RoundToInt(Time.time)}s");
    }
}
