using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FMODSoundtrack : MonoBehaviour
{
    [SerializeField] private string _selectSoundtrack;
    FMOD.Studio.EventInstance soundtrack;

    private void Awake()
    {
        soundtrack = FMODUnity.RuntimeManager.CreateInstance(_selectSoundtrack);
        soundtrack.start();
    }

    public void OnDestroy()
    {
        soundtrack.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        soundtrack.release();
    }
}
