using SharedData.Events.Generics;
using UnityEngine;

namespace SharedData.Events
{
    [CreateAssetMenu(fileName = "Transform Event", menuName = "Shared/Events/T1 Transform Event", order = 0)]
    public class TransformEvent : OneParameterEvent<Transform>
    {
    }
}