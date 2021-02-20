using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject _player;
    private float _zPosition=0;
    [SerializeField][Range(0,1)] private float _cameraStrictFollow=0.9f;
    [SerializeField] private float _cameraDistanceWitchSlimeGrowth = 0.75f;
    [SerializeField] private int _roundPrecision = 1;
    void Start()
    {
        _player = GameManager.Instance.GivePlayer();
        _zPosition = this.transform.position.z;
        GameManager.OnResize += GetMoreDistanceToPlayer;
    }
    private void GetMoreDistanceToPlayer(int value,int oldValue)
    {
        _zPosition -= (value- oldValue) * _cameraDistanceWitchSlimeGrowth;
    }
    // Update is called once per frame
    void Update()
    {
        if(_player==null)
        {
            _player = GameManager.Instance.GivePlayer();
        }
        this.transform.position = new Vector3(
            Mathf.Lerp(this.transform.position.x, (float)Math.Round(_player.transform.position.x, _roundPrecision), _cameraStrictFollow),
            Mathf.Lerp(this.transform.position.y, (float)Math.Round(_player.transform.position.y, _roundPrecision), _cameraStrictFollow),
            _zPosition
            );
    }
}
