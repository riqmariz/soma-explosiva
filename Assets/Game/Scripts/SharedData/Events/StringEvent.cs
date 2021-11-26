
using UnityEngine;
using SharedData.Events.Generics;

namespace SharedData.Events {
    /// <summary>
    /// An Event that takes a single int as Parameter
    /// </summary>
    [CreateAssetMenu(fileName = "String Event", menuName = "Shared/Events/T1 String Event", order = 0)]
    public class StringEvent : OneParameterEvent<string>
    {   
    }
}