using System;
using UnityEngine;
using UnityEngine.UI;

public class CompassController : MonoBehaviour
{
    [SerializeField]
    private Image[] _images = Array.Empty<Image>();
    [SerializeField]
    private Transform _origin;
    [SerializeField] 
    private float _maxDistance = 200f;
    [SerializeField] 
    private float _compassLimitDistance = 2f;
    [SerializeField]
    private RectTransform _indicator;

    private void Start()
    {
        _indicator.transform.position = transform.position;
    }

    public void VisualizeTarget(Vector2 direction,  float distance)
    {
        float fitting = distance / _maxDistance;
        if (fitting < 1)
        {
            _indicator.gameObject.SetActive(true);
            foreach (Image image in _images)
            {
                image.gameObject.SetActive(false);
            }
            
            float uiDistance = Mathf.Clamp01(fitting) * _compassLimitDistance;
            Vector2 translation = direction.normalized * uiDistance;
            _indicator.position = _origin.position + (Vector3) translation;
            Debug.Log("Translation " + translation.magnitude);
        }
        else
        {
            _indicator.gameObject.SetActive(false);
            foreach (Image image in _images)
            {
                image.gameObject.SetActive(false);
            }
            
            float atan = -Vector2.SignedAngle(Vector2.up, direction);
            if (atan < 0)
            {
                atan += 360f;
            }

            atan /= 360;
            int index = Mathf.Max(0, Mathf.RoundToInt(atan * _images.Length));
            Debug.Log("Image index " + index + " " + atan);
            _images[index].gameObject.SetActive(true);
        }

    }
}
