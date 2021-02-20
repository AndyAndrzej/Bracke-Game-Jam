using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFocus : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject _player;
    private float _zPosition=0;
    [SerializeField][Range(0,1)] private float _cameraStrictFollow=0.9f;
    [SerializeField] private float _cameraDistanceWitchSlimeGrowth = 0.75f;
    [SerializeField] private int _roundPrecision = 1;
    private CinemachineVirtualCamera _camera;
    private CinemachineTransposer _offeset;
    void Start()
    {
        
        _player = GameManager.Instance.GivePlayer();
        _camera = this.GetComponent<CinemachineVirtualCamera>();
        _offeset = _camera.GetCinemachineComponent<CinemachineTransposer>();
        _zPosition = _offeset.m_FollowOffset.z;
        GameManager.OnResize += GetMoreDistanceToPlayer;
    }
    private void GetMoreDistanceToPlayer(int value,int oldValue)
    {
        _zPosition -= (value- oldValue) * _cameraDistanceWitchSlimeGrowth;
        _offeset.m_FollowOffset.z = _zPosition;
    }
    private void Update()
    {
        _offeset.m_FollowOffset.z = Mathf.Lerp(_offeset.m_FollowOffset.z, _zPosition, _cameraStrictFollow);
    }
    // Update is called once per frame

}
