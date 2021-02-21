using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateRender : MonoBehaviour
{
    // Start is called before the first frame update
    private MeshRenderer[] _renders;
    void Start()
    {
        _renders = this.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < _renders.Length; i++)
        {
            _renders[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
