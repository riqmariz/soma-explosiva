using UnityEngine;
using Event = SharedData.Events.Event;

public class WinOnBossKill : MonoBehaviour
{
    [SerializeField] 
    private Event onBossKill;

    private void Awake()
    {
        onBossKill.AddCallback(LevelComplete);
    }

    private void OnDestroy()
    {
        onBossKill.RemoveCallback(LevelComplete);
    }

    private void LevelComplete()
    {
        PopupManager.GetInstance().ShowPopup<GameCompletePopup>();
    }
}
