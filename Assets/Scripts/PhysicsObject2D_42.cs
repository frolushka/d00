﻿using UnityEngine;

public class PhysicsObject2D_42 : MonoBehaviour
{
    public delegate void CollisionHandler(Physics2D_42.CollisionData data);
    public event CollisionHandler onCollision;
        
    public bool isStatic;
    public bool isTrigger;
    public Vector2 size;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnDrawGizmosSelected()
    {
        var p = new Vector2(transform.position.x, transform.position.y);
        Gizmos.color = isStatic ? Color.red : Color.green;
        Gizmos.DrawLine(p - size / 2, p + new Vector2(size.x, -size.y) / 2);
        Gizmos.DrawLine(p - size / 2, p + new Vector2(-size.x, size.y) / 2);
        Gizmos.DrawLine(p + size / 2, p + new Vector2(size.x, -size.y) / 2);
        Gizmos.DrawLine(p + size / 2, p + new Vector2(-size.x, size.y) / 2);
    }
    
    public void Move(Vector2 translation)
    {
        _transform.Translate(translation);
        Physics2D_42.Instance.IsDirty = true;
    }

    public void OnCollision(Physics2D_42.CollisionData data)
    {
        onCollision?.Invoke(data);
    }
}
