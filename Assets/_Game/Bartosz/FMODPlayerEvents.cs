using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class FMODPlayerEvents : MonoBehaviour
{
    [SerializeField] private string _jump;
    [SerializeField] private string _connect;
    FMOD.Studio.EventInstance JumpEvent;
    FMOD.Studio.EventInstance ConnectEvent;


    private void Start()
    {
        ConnectEvent = FMODUnity.RuntimeManager.CreateInstance(_connect);
        JumpEvent = FMODUnity.RuntimeManager.CreateInstance(_jump);
    }
    private void Update()
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(ConnectEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(JumpEvent, GetComponent<Transform>(), GetComponent<Rigidbody>());
    }

    void PlayJumpEvent()
    {
        JumpEvent.start();
    }

    void PlayConnectEvent()
    {
        ConnectEvent.start();
    }
}

