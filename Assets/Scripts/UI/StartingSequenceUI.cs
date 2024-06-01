using System;
using UnityEngine;
using UnityEngine.UI;

public class StartingSequenceUI : MonoBehaviour
{
    [SerializeField]
    private Image[] _images = Array.Empty<Image>();

    public void DisplayNumber(int number)
    {
        for (var index = 0; index < _images.Length; index++)
        {
            var image = _images[index];
            image.gameObject.SetActive(index < number);
        }
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
