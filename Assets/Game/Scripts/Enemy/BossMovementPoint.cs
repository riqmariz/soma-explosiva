using UnityEngine;

public class BossMovementPoint : MonoBehaviour
{
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(this.transform.position,new Vector3(0.4f,0.4f,0));
    }
}
