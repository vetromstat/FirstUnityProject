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
    private Player Pl;
    public float Health = 100;
    private int CopyHealth;

    private float maxMoveSpeed = 6;

    [SerializeField]
    TMP_Text hpLabel;

    public void Start()
    {

        target  = GameObject.FindWithTag("Player");
        CopyHealth = (int)Health;
        Pl = target.GetComponent<Player>();
        
        
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
            float dmg = Pl.WeaponIndex;
            Health -= (dmg + 2) * 10;
           
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Deals damage"))
        {
            float dmg = Pl.WeaponIndex;
            Health -= (dmg + 2) * 10;

        }
    }
    void HandleHP()
    {
        if (Health <= 0)
        {
            Pl.Points += CopyHealth;
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
            moveSpeed += 0.0025f;
        }
    }


}
