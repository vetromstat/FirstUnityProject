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

    private float maxMoveSpeed = 6;

    [SerializeField]
    TMP_Text hpLabel;

    public void Start()
    {

        target  = GameObject.FindWithTag("Player");
        CopyHealth = (int)Health;
        
        
    }
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        
    }
    public void Update()
    {
        HandleHP();
        HandleHPLabel();
        DifficultyScaler();
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Deals damage"))
        {
            float dmg = target.GetComponent<Player>().WeaponIndex;
            Health -= (dmg + 2) * 10;
           
        }
    }
    public void OnTriggerEnter(Collider other)
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
    void HandleHPLabel()
    {
        hpLabel.text = Health.ToString();
    }
    void DifficultyScaler()
    {
        if ( moveSpeed < maxMoveSpeed)
        {
            moveSpeed += 0.000001f;
        }
    }


}
