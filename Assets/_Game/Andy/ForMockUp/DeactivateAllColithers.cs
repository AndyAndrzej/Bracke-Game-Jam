using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAllColithers : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider[] _collithers;
    void Start()
    {
        _collithers = this.GetComponentsInChildren<BoxCollider>();
        for (int i = 0; i < _collithers.Length; i++)
        {
            _collithers[i].enabled = false;
            Destroy(_collithers[i], 2);
        }
    }


}
