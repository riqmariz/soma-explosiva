using SharedData.Events.Generics;
using UnityEngine;

namespace SharedData.Events
{ /// <summary>
    /// An Event that takes a single bool as Parameter
    /// </summary>
    [CreateAssetMenu(fileName = "Bool Event", menuName = "Shared/Events/T1 bool Event", order = 2)]
    public class BoolEvent : OneParameterEvent<bool>
    {   
    }
}