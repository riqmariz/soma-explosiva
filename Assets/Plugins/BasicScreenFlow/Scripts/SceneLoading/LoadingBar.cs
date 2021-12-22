using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    private void Awake()
    {
        SceneLoader.LoaderCallback();
    }

}
