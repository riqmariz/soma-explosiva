using System.Collections.Generic;
using UnityEngine;

namespace SharedData.Values
{
    [CreateAssetMenu(fileName = "Int Bool Dictionary Value", menuName = "Shared/Int Bool Dict", order = 0)]
    public class IntBoolDictionary : AbstractValue<Dictionary<int, bool>>
    {
        
    }
}