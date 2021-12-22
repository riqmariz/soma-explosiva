using System.Collections.Generic;
using UnityEngine;

public class BossPath : MonoBehaviour
{
    [SerializeField] 
    private int order;
    public int Order => order;

    private List<BossMovementPoint> _bossMovementPointList;
    public List<BossMovementPoint> BossMovementPointList => _bossMovementPointList;

    private void Awake()
    {
        _bossMovementPointList = new List<BossMovementPoint>();
        foreach (Transform child in transform)
        {
            var bmp = child.GetComponent<BossMovementPoint>();
            if (bmp)
            {
                _bossMovementPointList.Add(bmp);
            }
        }
    }
}
