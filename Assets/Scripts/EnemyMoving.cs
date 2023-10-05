using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMoving : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    private GameObject target;

    public void Start()
    {
        target  = GameObject.FindWithTag("Player");
    }
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);    
    }
}
