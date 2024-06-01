using System;
using UnityEngine;
using UnityEngine.UI;

public class EngineGaugeUI : MonoBehaviour
{
    [SerializeField] 
    private Image[] _frontImages = Array.Empty<Image>();
    [SerializeField] 
    private Image[] _backImages = Array.Empty<Image>();

    public void DisplayThrust(float completion)
    {
        if (completion < 0)
        {
            completion *= -2f;
            int count = Mathf.FloorToInt((completion * _backImages.Length) + 0.1f);
            foreach (var fr in _frontImages)
            {
                fr.gameObject.SetActive(false);
            }
            for (int i = 0; i < _backImages.Length; i++)
            {
                _backImages[i].gameObject.SetActive(i<count);
            }
        }
        else
        {
            int count = Mathf.FloorToInt((completion * _frontImages.Length) + 0.1f);
            foreach (var fr in _backImages)
            {
                fr.gameObject.SetActive(false);
            }
            for (int i = 0; i < _frontImages.Length; i++)
            {
                _frontImages[i].gameObject.SetActive(i<count);
            }
        }
    }
}
