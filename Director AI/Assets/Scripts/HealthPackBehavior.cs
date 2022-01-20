using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackBehavior : MonoBehaviour
{
    int _health = 20;
    PlayerCharacter _player = null;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerCharacter>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag =="Friendly")
        {
            Debug.Log(_player.GetComponent<Health>().CurrentHealth);
            if(_player.GetComponent<Health>().CurrentHealth < 100)
            {
                _player.GetComponent<Health>().CurrentHealth += _health;

                if (_player.GetComponent<Health>().CurrentHealth > 100)
                {
                    _player.GetComponent<Health>().CurrentHealth = 100;
                }

                Destroy(gameObject);
            }
        }
    }
}
