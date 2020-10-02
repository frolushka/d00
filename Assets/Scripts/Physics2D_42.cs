using System;
using UnityEngine;

public class Physics2D_42 : MonoBehaviour
{
    public struct CollisionData
    {
        public Vector2 normal;
        public GameObject other;

        public CollisionData(Vector2 normal, GameObject other)
        {
            this.normal = normal;
            this.other = other;
        }
    }
    
    private const float Threeshold = 0.001f;
    
    public static Physics2D_42 Instance { get; private set; }
    
    [SerializeField] private BoxCollider2D_42[] colliders;

    public bool Active { get; set; } = true;
    
    private void Awake()
    {
        if (colliders == null || colliders.Length == 0)
            enabled = false;
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (Active)
        {
            Simulate();
        }
    }

    private void Simulate()
    {
        for (var i = 0; i < colliders.Length - 1; i++)
        {
            for (var j = i + 1; j < colliders.Length; j++)
            {
                if (colliders[i].isStatic && colliders[j].isStatic)
                    continue;
                
                if (TryIntersectBoxColliders(colliders[i], colliders[j], out var normal))
                {
                    ICollisionHandler collisionHandler;
                    if (colliders[i].TryGetComponent<ICollisionHandler>(out collisionHandler))
                    {
                        collisionHandler.OnCollision(new CollisionData(-normal, colliders[j].gameObject));
                    }
                    if (colliders[j].TryGetComponent<ICollisionHandler>(out collisionHandler))
                    {
                        collisionHandler.OnCollision(new CollisionData(normal, colliders[i].gameObject));
                    }
                }
            }    
        }
    }

    public static bool TryIntersectBoxColliders(BoxCollider2D_42 first, BoxCollider2D_42 second, out Vector2 normal)
    {
        var fp = first.transform.position;
        var sp = second.transform.position;
        var fp2 = new Vector2(fp.x, fp.y);
        var fp3 = new Vector2(sp.x, sp.y);
        var a = fp2 - first.size / 2;
        var b = fp2 + first.size / 2;
        var c = fp3 - second.size / 2;
        var d = fp3 + second.size / 2;
        normal = Vector2.zero;
        if (!(a.x > d.x || c.x > b.x || a.y > d.y || c.y > b.y))
        {
            if (first.isTrigger || second.isTrigger)
                return true;
            
            if (a.x > c.x && a.x <= d.x && b.x > d.x)
            {
                if (!first.isStatic)
                {
                    fp.x = sp.x + (first.size.x + second.size.x) / 2 + Threeshold;
                    first.transform.position = fp;
                }
                else if (!second.isStatic)
                {
                    sp.x = fp.x - (first.size.x + second.size.x) / 2 - Threeshold;
                    second.transform.position = sp;
                }
                normal = Vector2.right;
            }
            else if (c.x > a.x && c.x <= b.x && d.x > b.x)
            {
                if (!first.isStatic)
                {
                    fp.x = sp.x - (first.size.x + second.size.x) / 2 - Threeshold;
                    first.transform.position = fp;
                }
                else if (!second.isStatic)
                {
                    sp.x = fp.x + (first.size.x + second.size.x) / 2 + Threeshold;
                    second.transform.position = sp;
                }
                normal = Vector2.left;
            }
            else if (a.y > c.y && a.y <= d.y && b.y > d.y)
            {
                if (!first.isStatic)
                {
                    fp.y = sp.y + (first.size.y + second.size.y) / 2 + Threeshold;
                    first.transform.position = fp;
                }
                else if (!second.isStatic)
                {
                    sp.y = fp.y - (first.size.y + second.size.y) / 2 - Threeshold;
                    second.transform.position = sp;
                }
                normal = Vector2.up;
            }
            else if (c.y > a.y && c.y <= b.y && d.y > b.y)
            {
                if (!first.isStatic)
                {
                    fp.y = sp.y - (first.size.y + second.size.y) / 2 - Threeshold;
                    first.transform.position = fp;
                }
                else if (!second.isStatic)
                {
                    sp.y = fp.y + (first.size.y + second.size.y) / 2 + Threeshold;
                    second.transform.position = sp;
                }
                normal = Vector2.down;
            }
            return true;
        }

        return false;
    }
}
