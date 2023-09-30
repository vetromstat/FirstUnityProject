using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTake : MonoBehaviour
{

    private float Health = 100; 
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
            float dmg = other.gameObject.GetComponent<Projectile>().GetDamage();
            TakeDamage(dmg);
            Debug.Log(Health);
        }
    }

    void TakeDamage(float damage)
    {
        Health -= damage;
    }

     void Heal(int heal)
    {
        Health += heal;
    }
}
