using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneMerge : Cutscene
{
    private BoneMapping _boneControl;
    private Transform _root;
    [SerializeField]private float _rotAnTime=0.1f,_jumpForce=5,_jumpAnTime=1,_sizePoints=2;
    [SerializeField] private Transform _mergedSlime;
    private void Start()
    {
        ReadyToPlay = true;
    }
    public override void Run(Vector3 _position, GameObject _slime)
    {
        ReadyToPlay = false;
        _boneControl = _slime.GetComponentInChildren<BoneMapping>();
        _root = _boneControl._rootRB.transform;
        _root.GetComponent<Jump>().canBeControled = false;
        _boneControl.SlowDown();
        _root.DORotate(Vector3.zero, _rotAnTime).OnStart(()=>
           {
               _mergedSlime.DOJump(_mergedSlime.transform.position, _jumpForce,1, _jumpAnTime).OnComplete(()=>
               {
                   _root.DOJump(_root.transform.position, _jumpForce, 1, _jumpAnTime).OnComplete(() =>
                   {
                       _mergedSlime.DOJump(_root.transform.position, _jumpForce, 1, _jumpAnTime).OnComplete(() =>
                       {
                           _mergedSlime.gameObject.SetActive(false);
                           GameManager.Instance.SizePoints += (int)_sizePoints;
                           _root.GetComponent<Jump>().canBeControled = true;
                       });
                   });
               }
               );
           } 
            );
        
        
    }


}
