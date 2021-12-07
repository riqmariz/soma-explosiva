using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] private int cacheSize = 8;

    private Stack<GenericPopup> _popups = new Stack<GenericPopup>();
    private bool _brightnessShown = false;
    [SerializeField]private LRUCache<GenericPopup> lRUCache;
    bool init = false;

    void Init()
    {
        lRUCache = new LRUCache<GenericPopup>(cacheSize);
        init = true;
    }

    public T ShowPopup<T>() where T : GenericPopup
    {
        if (!init)
        {
            Init();
        }
        T popupWindow = GetPopup<T>();
        Debug.Log(popupWindow);

        _popups.Push(popupWindow);
        popupWindow.transform.SetAsLastSibling();
        popupWindow.Show();

        return popupWindow;
    }

    public T ClosePopup<T>() where T : GenericPopup
    {
        Debug.Log("Close: "+ typeof(T));
        T popupWindow = (T)_popups.Pop();
        popupWindow.Hide();
        return popupWindow;
    }

    public T GetPopup<T>() where T : GenericPopup
    {
        if (!init)
        {
            Init();
        }
        
        //search in cache first
        var popup = lRUCache.FindEntryOfType<T>();

        if (popup == null)
        {
            //Debug.Log("Not in cache");
            var foundPopups = Resources.FindObjectsOfTypeAll<T>().Where(x => x.gameObject.scene.isLoaded).ToArray();
            if (foundPopups.Length > 0)
            {
                return (T)lRUCache.Access(foundPopups[0]);
            }
        }
        else
        {
            return (T)lRUCache.Access(popup);
        }

        Debug.LogError("Popup not found!");
        return null;
    }

    public bool AlreadyShownBrightnessPopup() => _brightnessShown;

    public void BrightnessPopupShowed() { _brightnessShown = true; }
}
