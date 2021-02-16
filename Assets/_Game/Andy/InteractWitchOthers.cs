using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWitchOthers : MonoBehaviour
{
    private IReaktable _temp;
    private void Start()
    {
        GameManager.Instance.AddPlayer(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out _temp))
        {
            _temp.TakeAktion(this.gameObject);
        }
    }
}
