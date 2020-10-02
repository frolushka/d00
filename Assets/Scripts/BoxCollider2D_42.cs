using System;
using UnityEngine;
using UnityEngine.Events;

public class BoxCollider2D_42 : MonoBehaviour
{
    public bool isStatic;
    public bool isTrigger;
    public Vector2 size;

    private void OnDrawGizmos()
    {
        var p = new Vector2(transform.position.x, transform.position.y);
        Gizmos.color = isStatic ? Color.red : Color.green;
        Gizmos.DrawLine(p - size / 2, p + new Vector2(size.x, -size.y) / 2);
        Gizmos.DrawLine(p - size / 2, p + new Vector2(-size.x, size.y) / 2);
        Gizmos.DrawLine(p + size / 2, p + new Vector2(size.x, -size.y) / 2);
        Gizmos.DrawLine(p + size / 2, p + new Vector2(-size.x, size.y) / 2);
    }
}
