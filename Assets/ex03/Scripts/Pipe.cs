using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    private const int RewardPoints = 5;

    private static float _speed;

    [SerializeField] private float defaultSpeed;
    [SerializeField] private float speedIncrease;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform outPoint;
    
    [SerializeField] private Bird player;

    private bool _passed;

    private void Start()
    {
        _speed = defaultSpeed;
    }

    private void Update()
    {
        if (Bird.GameOver)
            return;
        
        if (!_passed && player.transform.position.x > transform.position.x)
        {
            player.points += RewardPoints;
            _speed += speedIncrease;
            _passed = true;
        }

        if (outPoint.position.x > transform.position.x)
        {
            transform.position = spawnPoint.position;
            _passed = false;
        }
        
        transform.Translate(_speed * Time.deltaTime * Vector3.left);
    }
}
