﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneMapping : MonoBehaviour
{

    [SerializeField] private float _spring = 100, _damper = 0.2f, _rbAngularDrag = 10,_rbDrag=0;
    [SerializeField] private PhysicMaterial _material;
    [Tooltip("SizeOfColither")]
    [SerializeField]
    private float _size = 5;
    [Tooltip("MassOfeachBone")]
    [SerializeField]
    private float _mass = 10;
    [Tooltip("Position of Colither center beetwen bone(0) and root(1)")]
    [Range(0,1)]
    [SerializeField] private float _colCenterPosBetRootAndBone = 0.5f;
    [Tooltip("This as children need had a root bone")]
    [SerializeField] private Transform _armature;
    [Tooltip("Tojest liczba kości połączonych do jednej kości, wartość zawsze nieprzysta ")]
    [SerializeField] private int _bonesConnections = 7;
    private GameObject _root;
    private GameObject[] _bones;
    [HideInInspector]public Rigidbody _rootRB;
    [HideInInspector]public Rigidbody[] _bonesRB;
    private SphereCollider[] _bonesCol;
    private SpringJoint[] _bonesSpring;
    private JointLimits _limits;
    private RigidbodyConstraints _constraints;
    private void Awake()
    {
        _root = _armature.gameObject;
        _constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ| RigidbodyConstraints.FreezeRotationX;
        if (!_root.TryGetComponent(out _rootRB))
        {
            _rootRB = _root.AddComponent<Rigidbody>();
            _rootRB.constraints = _constraints;
        }
        _bones = new GameObject[_root.transform.childCount];
        _bonesRB = new Rigidbody[_root.transform.childCount];
        _bonesSpring = new SpringJoint[_root.transform.childCount* _bonesConnections];
        _bonesCol = new SphereCollider[_root.transform.childCount];
        for (int i = 0; i < _root.transform.childCount; i++)
        {
            _bones[i] = _root.transform.GetChild(i).gameObject;

            //add colison and main rigidbody
                _bonesRB[i] = _bones[i].AddComponent<Rigidbody>();
                _bonesRB[i].constraints = RigidbodyConstraints.FreezePositionZ;
                _bonesCol[i]=_bones[i].AddComponent<SphereCollider>();
                _bonesCol[i].radius = _size;
                _bonesCol[i].center = _bones[i].transform.InverseTransformPoint(_root.transform.position) * _colCenterPosBetRootAndBone;
            _bonesCol[i].material = _material;

                _bonesRB[i].mass = i< _root.transform.childCount/2 ? _mass*2:_mass;
                _bonesRB[i].angularDrag = _rbAngularDrag;
                _bonesRB[i].drag = _rbDrag;
        }
        SetJoints();
 
    }
    public void SetJoints()
    {
        for (int i = 0; i < _root.transform.childCount; i++)
        {
            _bonesRB[i].isKinematic = false;
            _bonesRB[i].useGravity = true;
        }

            int j = _root.transform.childCount;
        for (int k = -_bonesConnections / 2; k < _bonesConnections / 2 + 1; k++)
        {
            if (k != 0)
            {
                for (int i = 0; i < _root.transform.childCount; i++)
                {

                    _bonesSpring[j] = _bones[i].AddComponent<SpringJoint>();
                    _bonesSpring[j].connectedBody = _bonesRB[CircleValue(i + k, _root.transform.childCount)];
                    _bonesSpring[j].damper = _damper;
                    _bonesSpring[j].spring = _spring;

                    j++;
                }
            }
            else
            {
                for (int i = 0; i < _root.transform.childCount; i++)
                {
                    _bonesSpring[i] = _bones[i].AddComponent<SpringJoint>();
                    _bonesSpring[i].connectedBody = _rootRB;
                    _bonesSpring[i].damper = _damper;
                    _bonesSpring[i].spring = _spring;
                }
            }
        }
    }
    public void ClearJonts()
    {
        for (int i = 0; i < _root.transform.childCount; i++)
        {
            _bonesRB[i].angularVelocity = Vector3.zero;
            _bonesRB[i].velocity = Vector3.zero;
            _bonesRB[i].isKinematic = true;
            _bonesRB[i].useGravity = false;
        }
        for (int i = _bonesSpring.Length - 1; i >= 0; i--)
        {
            Destroy(_bonesSpring[i]);
        }
    }
    [SerializeField] private float _strenghtOfSlowInCutscene=250;
    public void SlowDown()
    {

        for (int i = 0; i < _root.transform.childCount; i++)
        {
            _bonesRB[i].angularVelocity = Vector3.zero;
            _bonesRB[i].velocity = Vector3.zero;
        }
    }
    private int CircleValue(int value,int max)
    {
        if(value>=max)
        {
            print(value - max + " "+value );
            return value-max ;
        }else if(value<0)
        {
            return max + value;
        }else
        {
            return value;
        }    
    }
}
