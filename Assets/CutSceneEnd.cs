using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEnd : Cutscene
{
    public override void Run(Vector3 _position, GameObject _slime)
    {
        GameManager.Instance.End = GameManager.EndGame.win;
    }

    // Start is called before the first frame update
    void Start()
    {
        _readyToPlay = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
