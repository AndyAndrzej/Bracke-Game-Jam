using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoneMapping))]
public class Jump : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _rootRB;
    [SerializeField] private float _force = 15;
    void Start()
    {
        _rootRB = this.GetComponent<BoneMapping>()._rootRB;
    }

    // Update is called once per frame
    private Vector3 _direction;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
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
            
        }
    }
}
