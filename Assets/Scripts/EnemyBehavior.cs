using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (other.gameObject.CompareTag("Deals damage"))
        {
            float dmg = target.GetComponent<Player>().WeaponIndex;
            Health -= (dmg + 2) * 10;
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
