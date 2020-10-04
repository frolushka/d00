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
    public bool IsDirty { get; set; }
    
    [SerializeField] private PhysicsObject2D_42[] colliders;

    public bool active = true;

    private void Awake()
    {
        if (colliders == null || colliders.Length == 0)
            active = false;
        Instance = this;
    }

    private void FixedUpdate()
    {
        if (active && IsDirty)
        {
            Simulate();
            IsDirty = false;
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
                    colliders[i].OnCollision(new CollisionData(-normal, colliders[j].gameObject));
                    colliders[j].OnCollision(new CollisionData(normal, colliders[i].gameObject));
                }
            }    
        }
    }

    private delegate void CollisionDelegate(out Vector2 normal);
    public static bool TryIntersectBoxColliders(PhysicsObject2D_42 first, PhysicsObject2D_42 second, out Vector2 normal)
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

            var min = float.MaxValue;
            CollisionDelegate cd = null;
            if (a.x > c.x && a.x <= d.x && b.x > d.x && d.x - a.x < min)
            {
                min = d.x - a.x;
                cd = OnRightCollision;
            }
            if (c.x > a.x && c.x <= b.x && d.x > b.x && b.x - c.x < min)
            {
                min = b.x - c.x;
                cd = OnLeftCollision;
            }
            if (a.y > c.y && a.y <= d.y && b.y > d.y && d.y - a.y < min)
            {
                min = d.y - a.y;
                cd = OnTopCollision;
            }
            if (c.y > a.y && c.y <= b.y && d.y > b.y && b.y - c.y < min)
            {
                min = b.y - c.y;
                cd = OnBotCollision;
            }
            cd?.Invoke(out normal);
            return true;
        }

        return false;

        void OnRightCollision(out Vector2 n)
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
            n = Vector2.right;
        }

        void OnLeftCollision(out Vector2 n)
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
            n = Vector2.left;
        }

        void OnTopCollision(out Vector2 n)
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
            n = Vector2.up;   
        }

        void OnBotCollision(out Vector2 n)
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
            n = Vector2.down;
        }
    }
}
