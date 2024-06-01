using UnityEngine;

public class CreditScroll : MonoBehaviour
{
    [SerializeField] 
    private RectTransform _rectTransform;
    [SerializeField]
    private float _speed;
    void Update()
    {
        _rectTransform.position += Time.deltaTime * _speed * Vector3.up;
    }
}
