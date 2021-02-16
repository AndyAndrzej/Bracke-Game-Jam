using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jump : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _rootRB;
    private Rigidbody[] _bonesRB;
    [SerializeField] private float _force = 15;
    private SpringJoint _fixedjoit;
    [SerializeField] private float _breakingForce = 25, _tolerance = 0.1f, _maxDistance = 2,_spring=250;
    void Start()
    {
        _rootRB = this.GetComponentInParent<BoneMapping>()._rootRB;
        _bonesRB= this.GetComponentInParent<BoneMapping>()._bonesRB;
        
        

    }

    // Update is called once per frame
    private Vector3 _direction;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _direction = Vector3.up;
            if(Input.GetKey(KeyCode.A))
            {
                _direction += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _direction += Vector3.right;
            }
            _rootRB.AddForce(_direction * _force, ForceMode.Impulse);
            for (int i = 0; i < _bonesRB.Length; i++)
            {
                _bonesRB[i].AddForce(_direction * _force, ForceMode.Impulse);
            }


        }
    }
    private Rigidbody _temp;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out _temp) && !other.gameObject.name.Contains("bone"))
        {
            _fixedjoit = _rootRB.gameObject.AddComponent<SpringJoint>();
            _fixedjoit.breakForce = _breakingForce;
            _fixedjoit.connectedBody = _temp;
            _fixedjoit.spring = _spring;

            Debug.Log(_fixedjoit);


        }
    }

}
