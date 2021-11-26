using UnityEngine;
using SharedData.Events.Generics;

namespace SharedData.Events
{
    /// <summary>
    /// An Event that takes a single int as Parameter
    /// </summary>
    [CreateAssetMenu(fileName = "Int Event", menuName = "Shared/Events/T1 Int Event", order = 0)]
    public class IntEvent : OneParameterEvent<int>
    {
    }
}