using System;
using UnityEngine;
using UnityEngine.UI;

public class LeanGauge : MonoBehaviour
{
    [SerializeField] 
    private Image[] _images = Array.Empty<Image>();

    public void DisplayLean(float completion)
    {
        foreach (var image in _images)
        {
            image.gameObject.SetActive(false);
        }
        int count = Mathf.FloorToInt((completion * (_images.Length+1f)) + 0.1f);
        count--;
        if (count >= 0f)
        {
            _images[Mathf.Min(count, _images.Length-1)].gameObject.SetActive(true);
        }
    }
}
