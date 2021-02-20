using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cutscene: MonoBehaviour
{
    public delegate void ReadyToPlayEvent(bool active);
    public event ReadyToPlayEvent OnChangeActive;
    protected bool _readyToPlay=false;

    public bool ReadyToPlay { get => _readyToPlay; set { _readyToPlay = value; OnChangeActive(value); } }

    public abstract void Run(Vector3 _position, GameObject _slime);
}
