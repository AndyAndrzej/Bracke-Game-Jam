using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barier : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _forcetoDsetroy = 5,_sizeToDestroy=1;
    private FixedJoint[] _joints;
    void Start()
    {
        _joints = this.GetComponentsInChildren<FixedJoint>();
    }
    private void OnEnable()
    {
        GameManager.OnResize += MadeDestroyable;
    }
    private void OnDisable()
    {
        GameManager.OnResize -= MadeDestroyable;
    }
    // Update is called once per frame
    private void MadeDestroyable(int value,int oldValue)
    {
        if(value>= _sizeToDestroy)
        {
            for (int i = 0; i < _joints.Length; i++)
            {
                _joints[i].breakForce = _forcetoDsetroy;
            }
        }
    }
}
