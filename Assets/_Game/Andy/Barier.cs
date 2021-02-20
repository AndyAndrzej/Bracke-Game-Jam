using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barier : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _forcetoDsetroy = 5,_sizeToDestroy=1,_streghtenOnSmaller=10000;
    private FixedJoint[] _joints;
    void Start()
    {
        _joints = this.GetComponentsInChildren<FixedJoint>();
    }
    private void OnEnable()
    {
        GameManager.OnResize += MadeDestroyable;
        GameManager.OnSliced += PreventDestroy;
    }
    private void OnDisable()
    {
        GameManager.OnResize -= MadeDestroyable;
        GameManager.OnSliced -= PreventDestroy;
    }
    // Update is called once per frame
    private void MadeDestroyable(int value,int oldValue)
    {
        if(value>= _sizeToDestroy)
        {
            for (int i = 0; i < _joints.Length; i++)
            {
                _joints[i].breakForce = _forcetoDsetroy;
                if (GameManager.Instance.IsSliced)
                { _joints[i].breakForce *= _streghtenOnSmaller; }
            }
        }
    }
    private void PreventDestroy(bool yes)
    {
        for (int i = 0; i < _joints.Length; i++)
        {
            if (_joints[i].breakForce == _forcetoDsetroy && yes)
            {
                _joints[i].breakForce *= _streghtenOnSmaller;
            }

            if(_joints[i].breakForce == _forcetoDsetroy* _streghtenOnSmaller && !yes)
            {
                _joints[i].breakForce /= _streghtenOnSmaller;
            }
        }
    }
}
