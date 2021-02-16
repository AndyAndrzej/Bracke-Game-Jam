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
    private void JumpNow()
    {
            _jumpStrenght = Mathf.InverseLerp(0.1f, _jumpForceGrowthTime, Time.time - _timePressed);

            for (int i = 0; i < _bonesRB.Length; i++)
            {
                _bonesRB[i].AddForce((Vector3.right*_moveDirection)* _directionalForce * _jumpStrenght, ForceMode.Impulse);

            }
        float y= _environment.transform.position.y;
        _environment.transform.DOMoveY(_environment.transform.position.y + _force * _anTime * _jumpStrenght, _anTime).OnComplete(() => {
            _environment.transform.DOMoveY(y, _anTime);
        });
            _jumpStrenght = 0;
            _timePressed = 0;
    }

}
