using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeSizeControl : MonoBehaviour
{
    private float _startZPos;
    [SerializeField] private float _scaleGrowth;
    [SerializeField] private float _anTime = 2;
    private Vector3 _pos;
    private void Awake()
    {
        _startZPos = this.transform.parent.position.z;
    }
    public void ChangeSize()
    {
        
        this.transform.parent.DOMoveZ(_startZPos - _scaleGrowth * GameManager.Instance.SizePoints, _anTime);

    }
}
