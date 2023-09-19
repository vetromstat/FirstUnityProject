using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class Projectile : MonoBehaviour
{
    private Vector3 firingPoint;

    [SerializeField]
    float projectileSpeed;

    [SerializeField]
    float maxProjectileDistance;

    private bool shouldMove = false;
    void Start()
    {
       
    }


    void Update()
    {   
         if (shouldMove)
        {
            MoveProjectile();
        }
    }
    public void Move()
    {
        firingPoint = transform.position;
        shouldMove = true;
    }
    void MoveProjectile()
    {
        
        if (Vector3.Distance(firingPoint, transform.position) > maxProjectileDistance)
        {
            ProjectilePool.Instance.ReturnToPool(this);
            shouldMove = false; 
        }else 
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
        
        
    }
}
