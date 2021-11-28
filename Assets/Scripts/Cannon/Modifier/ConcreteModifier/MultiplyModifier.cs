using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new multiply modifier", menuName = "Modifiers/Multiply")]
public class MultiplyModifier : BaseModifier
{
    [SerializeField]
    private int MultiplyValue;

    public override int ModifierMethod(int value)
    {
        return value * MultiplyValue;
    }

    private void OnValidate()
    {
        UIDescription = "X" + MultiplyValue;
    }
}
