using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkieletAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _ignoreBelow = 3, _instaKill = 8, _killFromAboveFrom = 5, _alwaysKill = 10;
    private float _currentSlimeSize = 0;
    void Start()
    {
        GameManager.OnResize += OnSizeChange;
    }

    // Update is called once per frame
    private void OnSizeChange(int value, int oldvalue)
    {
        _currentSlimeSize = value;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.gameObject.name);
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<InteractWitchOthers>(out _) && _currentSlimeSize >= _ignoreBelow)
        {
            if (_currentSlimeSize <= _instaKill)
            {
                GameManager.Instance.End = GameManager.EndGame.Lose;
            }
            else if (_currentSlimeSize >= _alwaysKill)
            {
                GameManager.Instance.enemiesDefeated++;
                Destroy(this.gameObject);
            }
        }
        if (other.transform.TryGetComponent<InteractWitchOthers>(out _) && _currentSlimeSize >= _killFromAboveFrom)
        {
            if (other.TryGetComponent<Rigidbody>(out Rigidbody _rb) && _rb.velocity.y<=0)
            {
                GameManager.Instance.enemiesDefeated++;
                Destroy(this.gameObject);
            }

        }
    }
}
