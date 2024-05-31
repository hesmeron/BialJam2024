using UnityEngine;

public class CompassController : MonoBehaviour
{
    [SerializeField] 
    private float _maxDistance = 200f;
    [SerializeField] 
    private float _compassLimitDistance = 2f;
    [SerializeField]
    private RectTransform _indicator;
    
    public void VisualizeTarget(Vector2 direction,  float distance)
    {
        float uiDistance = Mathf.Clamp01(distance / _maxDistance) * _compassLimitDistance;
        Vector2 translation = direction * uiDistance;
        _indicator.transform.position = transform.position + (Vector3) translation;
    }
}
