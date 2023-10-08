using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMoving : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    private GameObject target;
    private float Health = 100;

    public void Start()
    {
        target  = GameObject.FindWithTag("Player");
    }
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        HandleHP();
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        { 
            float dmg = other.gameObject.GetComponent<Projectile>().GetDamage();
            Health -= dmg;
            Debug.Log(Health);
        }
    }

    void HandleHP()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
