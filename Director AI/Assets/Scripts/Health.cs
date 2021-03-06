using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Health : MonoBehaviour
{
    [SerializeField]
    private int _startHealth = 10;

    private int _currentHealth = 0;

    void Awake()
    {
        _currentHealth = _startHealth;
    }

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public void Damage(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
            Kill();
    }

    void Kill()
    {
        if(this.tag == "Enemy")
        {
          
            PlayerCharacter player = FindObjectOfType<PlayerCharacter>();
            if(player != null)
            {
                float distance = (player.transform.position - transform.position).sqrMagnitude;
                if ( distance <= 8.0f )
                {
                    player.Intensity += 0.04f;
                }

                else if(distance <= 30.0f)
                {
                    player.Intensity += 0.01f;
                }
            }
            
            DirectorAIBehavior.Instance.DecreaseEnemiesAlive();
        }
        Destroy(gameObject);
    }
}


