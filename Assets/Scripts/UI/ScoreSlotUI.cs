using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSlotUI : MonoBehaviour
{
    [SerializeField] 
    private Image _activeImage;

    private void Start()
    {
        Deactivate();
    }
    
    public void Activate()
    {
        _activeImage.gameObject.SetActive(true);
    }    
    
    public void Deactivate()
    {
        _activeImage.gameObject.SetActive(false);
    }
}
