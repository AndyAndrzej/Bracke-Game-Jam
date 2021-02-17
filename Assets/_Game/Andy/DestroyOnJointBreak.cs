using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnJointBreak : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _timeToDisapier = 3;

    private void OnJointBreak(float breakForce)
    {
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        Destroy(this.gameObject, _timeToDisapier);
    }
}
