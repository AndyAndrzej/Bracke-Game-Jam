using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeSizeControl : MonoBehaviour
{
    private float _startZPos;
    [SerializeField] private float _scaleGrowth;
    [SerializeField] private float _anTime = 2;
    [SerializeField] private BoneMapping _boneControl;
    public GameObject childSlime;
    private Vector3 _pos;
    private void Awake()
    {
        _startZPos = _boneControl.transform.localScale.z;


    }
    private void OnEnable()
    {
        GameManager.OnResize += ChangeSize;
    }
    private void OnDisable()
    {
        GameManager.OnResize -= ChangeSize;
    }
    public void ChangeSize(int value,int OldValue)
    {
        if (!GameManager.Instance.IsSliced)
        {
            Resize(value);
        }
        else
        {
            GameObject _temp = Instantiate(childSlime, this.transform.position, Quaternion.identity);
            _temp.transform.DOJump(_temp.transform.position, 5, 1, 0.1f);
        }
    }
    public void Resize(int value)
    {
        _boneControl.ClearJonts();
        //this.transform.parent.DOMoveZ(_startZPos - _scaleGrowth * value, _anTime);
        this.transform.DOMoveY(this.transform.position.y + _scaleGrowth * value * 2, _anTime).OnStart(() =>
        {
            this.transform.DOScale(_startZPos + _scaleGrowth * value, _anTime);
            _boneControl.transform.DOScale(_startZPos + _scaleGrowth * value, _anTime).OnComplete(() =>
            {
                _boneControl.SetJoints();
            });
        });
    }
}
