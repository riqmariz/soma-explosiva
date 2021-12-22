using UnityEngine;
using SharedData.Events.Generics;

namespace SharedData.Events
{
    /// <summary>
    /// An Event that takes a single int as Parameter
    /// </summary>
    [CreateAssetMenu(fileName = "Float Event", menuName = "Shared/Events/T1 Float Event", order = 0)]
    public class FloatEvent : OneParameterEvent<float>
    {
    }
}