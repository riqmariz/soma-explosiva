using UnityEngine;
using System.IO;
using System;

public class ScreenshotManager : MonoBehaviour
{
    private string _caminho;
    void Awake()
    {
        DontDestroyOnLoad(this);
        _caminho = Application.persistentDataPath + "/Capturas/";

        if (!Directory.Exists(_caminho))
        {
            Directory.CreateDirectory(_caminho);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Print))
        {
            string nomeImagem = _caminho + "captura_"+DateTime.Now.Ticks.ToString() + ".png";
            ScreenCapture.CaptureScreenshot(nomeImagem,2);
            Debug.Log("screenshot taken on: "+_caminho);
        }
    }
}
