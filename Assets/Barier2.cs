using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barier2 : MonoBehaviour
{
    [SerializeField] private float  _sizeToDestroy = 1;
    private Animator _anim;
    private bool _sizeReached = false;
    void Start()
    {
        _anim = this.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        GameManager.OnResize += MadeDestroyable;
    }
    private void OnDisable()
    {
        GameManager.OnResize -= MadeDestroyable;
    }
    // Update is called once per frame
    private void MadeDestroyable(int value, int oldValue)
    {
        _sizeReached = value >= _sizeToDestroy ? true : false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<InteractWitchOthers>(out _) && _sizeReached && !GameManager.Instance.IsSliced)
        {
            _anim.SetTrigger("Run");
            Destroy(this.gameObject, 2);
        }
    }
}
