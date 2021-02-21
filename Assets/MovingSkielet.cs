using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSkielet : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _from=225, _to=325,_moveTime=10,_rotateTime=2;
    void Start()
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOLocalMoveX(_from, _moveTime))
            .Append(transform.DOLocalRotate(Vector3.up*180, _rotateTime))
            .Append(transform.DOLocalMoveX(_to, _moveTime))
            .Append(transform.DOLocalRotate(Vector3.zero, _rotateTime))
            .Append(transform.DOLocalMoveX(this.transform.localPosition.x, _moveTime));
        mySequence.SetLoops(-1);
        this.GetComponent<Animator>().SetBool("walk", true);
        mySequence.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
