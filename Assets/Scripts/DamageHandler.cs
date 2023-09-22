using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTake : MonoBehaviour
{

    private int Health = 100; 
    // Start is called before the first frame update
    void Update()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Projectile"))
        {
            TakeDamage(10);
            Debug.Log(Health);
        }
    }

    void TakeDamage(int damage)
    {
        Health -= damage;
    }

     void Heal(int heal)
    {
        Health += heal;
    }
}
