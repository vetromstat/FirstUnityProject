using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class EnemyMoving : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    private GameObject target;
    public float Health = 100;
    private int CopyHealth; 

    public void Start()
    {

        target  = GameObject.FindWithTag("Player");
        CopyHealth = (int)Health;
         

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
           
        }
    }

    void HandleHP()
    {
        if (Health <= 0)
        {
            target.GetComponent<Player>().Points += CopyHealth;
            Destroy(gameObject);
        }
           

    }
}
