using UnityEngine;
using Event = SharedData.Events.Event;

public class BossPathChangerOnHit : MonoBehaviour
{
    [SerializeField] 
    private Event onValidHitOnBoss;

    [SerializeField] 
    private Event changeBossPath;

    private void Awake()
    {
        onValidHitOnBoss.AddCallback(ChangeBossPathEvent);
    }

    private void OnDestroy()
    {
        onValidHitOnBoss.RemoveCallback(ChangeBossPathEvent);
    }
    private void ChangeBossPathEvent()
    {
        Debug.Log("Change boss path event");
        changeBossPath.Raise();
    }
    
}
