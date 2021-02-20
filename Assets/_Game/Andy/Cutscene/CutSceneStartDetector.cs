using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneStartDetector : MonoBehaviour
{
    private bool _ready = false;
    private MeshRenderer _render;
    public Cutscene _cutscene;
    public Material _green, _yellow;

    private void Start()
    {
        _render = this.GetComponent<MeshRenderer>();
        ChangeState(_cutscene.ReadyToPlay);
        _cutscene.OnChangeActive += ChangeState;
    }

    public void ChangeState(bool active)
    {

         _render.sharedMaterial =active? _green:_yellow;
         _ready = active;
    }
    private InteractWitchOthers _slimeRef;
    private void OnTriggerEnter(Collider other)
    {
            if(_ready && other.TryGetComponent(out _slimeRef))
            {
            _cutscene.Run(this.transform.position, _slimeRef.transform.parent.gameObject);
            }
    }
}
