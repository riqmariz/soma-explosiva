using System.Collections.Generic;
using SharedData.Values;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBarUI : MonoBehaviour
{
    [SerializeField] 
    private IntValue bossHP;
    [SerializeField] 
    private Transform heartParent;
    [SerializeField] 
    private GameObject heartPrefab;
    [SerializeField] 
    private Sprite disabledHeartSprite;

    private Stack<Image> _heartImages;
    private int maxHP;
    private void Awake()
    {
        Clear();
        Create();
        bossHP.AddOnValueChanged(UpdateHeartCount);
    }
    private void OnDestroy()
    {
        bossHP.RemoveOnValueChanged(UpdateHeartCount);
    }
    
    //todo improvement here, it smells very bad
    void UpdateHeartCount(int hpNewValue)
    {
        if (hpNewValue != maxHP)
        {
            var removed = _heartImages.Pop();
            removed.sprite = disabledHeartSprite;
        }
    }
    void Create()
    {
        _heartImages = new Stack<Image>();
        maxHP = FindObjectOfType<BossHP>().BossMaxHP;
        for (int i = 0; i < maxHP; i++)
        {
            var heart = Instantiate(heartPrefab, heartParent);
            var heartSprite = heart.GetComponent<Image>();
            _heartImages.Push(heartSprite);
        }
    }
    void Clear()
    {
        foreach (Transform child in heartParent)
        {
            Destroy(child.gameObject);
        }
    }
    
}
