using UnityEngine;
using Utility;
using Event = SharedData.Events.Event;
public class BossFlashAnimationOnValidHit : MonoBehaviour
{
    [SerializeField] 
    private Event onValidHitOnBoss;
    [SerializeField] 
    private int flashesCount = 12;

    private void Awake()
    {
        onValidHitOnBoss.AddCallback(FlashAnimation);
    }

    private void OnDestroy()
    {
        onValidHitOnBoss.RemoveCallback(FlashAnimation);
    }

    private void FlashAnimation()
    {
        gameObject.GetComponent<Renderer>().Flash(BossHP._InvulnerableTime/flashesCount, flashesCount);
    }
}
