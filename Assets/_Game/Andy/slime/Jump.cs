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
    [SerializeField] public GameObject _environment;
    [SerializeField] private BoneMapping _boneControl;
    [HideInInspector] public bool canBeControled = true;
    [SerializeField] private float _slimeSizeModForVer=1, _slimeSizeModForHor=1;

    void Start()
    {
        _rootRB = _boneControl._rootRB;
        _bonesRB= _boneControl._bonesRB;
    }
    private float _moveDirection;
    // Update is called once per frame
    private float _jumpStrenght = 0,_timePressed;
    [SerializeField] private float _anTime = 0.1f;
    private float _restTime=4, _tempTime=0;
    void Update()
    {
        if (canBeControled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _timePressed = Time.time;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                JumpNow();
                this.GetComponent<FMODPlayerEvents>().PlayJumpEvent();
            }
            _moveDirection = Input.GetAxis("Horizontal");
            if(Input.GetKeyDown(KeyCode.R)&& Mathf.Abs(_tempTime-Time.time)>_restTime)
            {
                _tempTime = Time.time;
                GameManager.Instance.IsSliced = !GameManager.Instance.IsSliced;
                this.GetComponent<FMODPlayerEvents>().PlayConnectEvent();
            }
        }
    }
    private Vector3 _jumpHeight;
    private void JumpNow()
    {
            _jumpStrenght = Mathf.InverseLerp(0.1f, _jumpForceGrowthTime, Time.time - _timePressed);

           for (int i = 0; i < _bonesRB.Length; i++)
            {
                _bonesRB[i].AddForce((Vector3.right*_moveDirection) * (_directionalForce + (_slimeSizeModForHor * GameManager.Instance.SizePoints)) * _jumpStrenght, ForceMode.Impulse);

            }
        Vector3 y= _environment.transform.position;
        _jumpHeight = (Vector3.up * (_force + (_slimeSizeModForVer * GameManager.Instance.SizePoints))) * _anTime * _jumpStrenght;
        _environment.transform.DOMove(_environment.transform.position + _jumpHeight, _anTime)
            .OnComplete(() => {
            _environment.transform.DOMove(y, _anTime);
        });
        
        /*_environment.transform.DOPunchPosition(, _anTime,4);*/
            _jumpStrenght = 0;
            _timePressed = 0;
    }

}
