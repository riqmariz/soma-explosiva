using System.Collections.Generic;
using UnityEngine;
using Utility;
using Event = SharedData.Events.Event;
public class BossFlashAnimationOnValidHit : MonoBehaviour
{
    [SerializeField] 
    private Event onValidHitOnBoss;
    [SerializeField] 
    private int flashesCount = 12;
    [SerializeField] 
    private List<Renderer> rendererList;

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
        foreach (var renderer in rendererList)
        {
            renderer.Flash(BossHP._InvulnerableTime/flashesCount, flashesCount);
        }
    }
}
