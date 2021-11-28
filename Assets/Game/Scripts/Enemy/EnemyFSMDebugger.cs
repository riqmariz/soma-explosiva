
using UnityEditor;
using UnityEngine;
public class EnemyFSMDebugger : MonoBehaviour
{
    private EnemyController _enemyController;
    public void OnDrawGizmos()
    {
        #region draw
        
        #if UNITY_EDITOR
        _enemyController = gameObject.GetComponent<EnemyController>();
        
        string _stateText = _enemyController.EnemyFSM?.GetCurrentState().Name;
        
        GUIStyle customStyle = new GUIStyle();
        customStyle.fontSize = 30;   // can also use e.g. <size=30> in Rich Text
        customStyle.richText = true;
        Vector3 textPosition = transform.position + (Vector3.up * 0.3f);
        string richText = "<color=red><B>[" + _stateText + "]</B></color>";
        
        Handles.Label(textPosition, richText, customStyle);

        #endif
        #endregion
            
        
        
    }
}