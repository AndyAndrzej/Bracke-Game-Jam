using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jump : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _rootRB;
    private Rigidbody[] _bonesRB;
    [Header("Siła skoku")]
    [SerializeField] private float _force = 5,_directionalForce=25;
    [Header("Czas po którym osiąga maks moc skoku")]
    [SerializeField][Range(0,5)] private float _jumpForceGrowthTime = 1;
    [SerializeField] private GameObject _environment;

    void Start()
    {
        _rootRB = this.GetComponentInParent<BoneMapping>()._rootRB;
        _bonesRB= this.GetComponentInParent<BoneMapping>()._bonesRB;
    }
    private float _moveDirection;
    // Update is called once per frame
    private float _jumpStrenght = 0,_timePressed;
    [SerializeField] private float _anTime = 0.1f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _timePressed = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            JumpNow();
        }
        _moveDirection = Input.GetAxis("Horizontal");
    }
    private Vector3 _jumpHeight;
    private void JumpNow()
    {
            _jumpStrenght = Mathf.InverseLerp(0.1f, _jumpForceGrowthTime, Time.time - _timePressed);

           for (int i = 0; i < _bonesRB.Length; i++)
            {
                _bonesRB[i].AddForce((Vector3.right*_moveDirection)* _directionalForce * _jumpStrenght, ForceMode.Impulse);

            }
        Vector3 y= _environment.transform.position;
        _jumpHeight = (Vector3.up * _force) * _anTime * _jumpStrenght;
        _environment.transform.DOMove(_environment.transform.position + _jumpHeight, _anTime)
            .OnComplete(() => {
            _environment.transform.DOMove(y, _anTime);
        });
        
        /*_environment.transform.DOPunchPosition(, _anTime,4);*/
            _jumpStrenght = 0;
            _timePressed = 0;
    }

}
