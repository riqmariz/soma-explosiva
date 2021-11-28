using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new sum modifier",menuName = "Modifiers/Sum")]
public class SumModifier : BaseModifier
{
    [SerializeField]
    private int SumValue;

    public override int ModifierMethod(int value)
    {
        return value + SumValue;
    }

    private void OnValidate()
    {
        UIDescription = "";
        if (SumValue >= 0)
            UIDescription += "+";

        UIDescription += SumValue;
    }
}
