using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelNode : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] 
    private int index;
    public int Index => index;
    [SerializeField] 
    private string sceneLevelName;
    public string SceneLevelName => sceneLevelName;
    
    [SerializeField]
    private bool available;

    [SerializeField] 
    private List<Renderer> rendererList;

    public bool Available
    {
        get => available;
        set
        {
            available = value;
            OnChangeAvailable(available);
        }
    }

    private LevelSelectManager _levelSelectManager;

    private void Awake()
    {
        sceneLevelName = GameManager.GetInstance().LevelSceneNames[index];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("level clicked: "+index+", available: "+available);
        _levelSelectManager.Selected(this);
    }
    public void SetLevelSelectManager(LevelSelectManager levelSelectManager)
    {
        _levelSelectManager = levelSelectManager;
    }

    public void OnChangeAvailable(bool available)
    {
        if (available)
        {
            foreach (var r in rendererList)
            {
                r.material = _levelSelectManager.AvailableMaterial;
            }
        }
        else
        {
            foreach (var r in rendererList)
            {
                r.material = _levelSelectManager.NotAvailableMaterial;
            }
        }
    }
}