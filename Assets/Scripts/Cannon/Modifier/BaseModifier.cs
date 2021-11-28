using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BaseModifier : ScriptableObject
{
    public abstract int ModifierMethod(int value);
    public string UIDescription;
}
