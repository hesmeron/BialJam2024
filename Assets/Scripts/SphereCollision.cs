using UnityEngine;

public class SphereCollision : MonoBehaviour
{
    [SerializeField]
    private float _radius =1f;
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 0f, 1f, 0.4f);
        Gizmos.DrawSphere(transform.position, _radius);
        Gizmos.color = new Color(0f, 0f, 1f, 1f);
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
