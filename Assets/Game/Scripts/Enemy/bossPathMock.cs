
using UnityEngine;
using Event = SharedData.Events.Event;

public class bossPathMock : MonoBehaviour
{
    [SerializeField]
    private Event changeBossPath;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Debug.Log("Changing boss path on enter input");
            changeBossPath.Raise();
        }
    }
}
