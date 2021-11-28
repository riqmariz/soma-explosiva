using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class ModifierManager : MonoBehaviour
{
    public class ModifierListUpdatedEvent : UnityEvent<List<BaseModifier>> { }

    public ModifierListUpdatedEvent ModifierListUpdated = new ModifierListUpdatedEvent();

    [SerializeField]
    private List<BaseModifier> modifiers;

    [SerializeField]
    public BaseModifier CurrentModifier 
    {
        get 
        {
            if (modifiers != null && modifiers.Count > 0)
                return modifiers[0];
            return null;
        }
    }


    public List<string> ModifiersDescriptions => modifiers.Select(x => x.UIDescription).ToList();

    public void SwitchModifiers() 
    {
        var firstMod = modifiers[0];
        modifiers.RemoveAt(0);
        modifiers.Insert(modifiers.Count, firstMod);
        ModifierListUpdated.Invoke(modifiers);
    }
}
