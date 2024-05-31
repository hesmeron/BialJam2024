using UnityEngine;

public class SphereCollision : MonoBehaviour
{
    [SerializeField]
    private float _radius =1f;

    public float Radius => _radius;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 0f, 1f, 0.4f);
        Gizmos.DrawSphere(transform.position, _radius);
        Gizmos.color = new Color(0f, 0f, 1f, 1f);
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public bool IsInRange(Vector2 position)
    {
        return Vector2.Distance(position, (Vector2)transform.position) <= _radius;
    }
    
    public bool IsInRange(Vector2 position, float distance)
    {
        return Vector2.Distance(position, (Vector2)transform.position) <= (_radius + distance);
    }
}
