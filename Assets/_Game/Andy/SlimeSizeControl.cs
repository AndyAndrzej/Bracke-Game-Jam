using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeSizeControl : MonoBehaviour
{
    [SerializeField] private float _startScale;
    [SerializeField] private float _scaleGrowth;
    [SerializeField] private float _anTime = 2;
    public void ChangeSize()
    {
        this.transform.parent.DOScale(_startScale + _scaleGrowth * GameManager.Instance.SizePoints, _anTime);
    }
}
