using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Image loadingImage;

    private bool _isFirstUpdate = true;

    private void Update()
    {
        if (_isFirstUpdate)
        {
            SceneLoader.LoaderCallback();
            _isFirstUpdate = false;
        }

        GetLoadingProgress();
    }

    private void GetLoadingProgress()
    {
        loadingImage.fillAmount = SceneLoader.GetLoadingProgress();
    }
}
