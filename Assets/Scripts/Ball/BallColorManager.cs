using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColorManager : MonoBehaviour
{
    [SerializeField] private List<Material> m_colors;
    [SerializeField] private List<int> values;
    // Start is called before the first frame update
    
    public void SetColor(MeshRenderer meshRenderer, int value)
    {
        var colorIndex = values.IndexOf(value);
            
        meshRenderer.material = m_colors[colorIndex];
    }
}
