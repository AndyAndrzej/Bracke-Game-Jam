using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneMerge : Cutscene
{
    private BoneMapping _boneControl;
    private void Start()
    {
        _readyToPlay = true;
    }
    public override void Run(Vector3 _position, GameObject _slime)
    {
        _boneControl = _slime.GetComponentInChildren<BoneMapping>();
        _boneControl.SlowDown(true);
        //TODO
        _boneControl.SlowDown(false);
        ReadyToPlay = false;
    }


}
