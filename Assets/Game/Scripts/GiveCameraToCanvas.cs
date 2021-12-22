using UnityEngine;
public class GiveCameraToCanvas : MonoBehaviour
{
    private void OnValidate()
    {
        GetCameraAndGiveToCanvas();
    }

    private void Awake()
    {
        GetCameraAndGiveToCanvas();
    }

    private void GetCameraAndGiveToCanvas()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }
}
