using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] 
    private Canvas _canvas;
    
    public void Initialize(Camera camera)
    {
        _canvas.worldCamera = camera;
    }
}
