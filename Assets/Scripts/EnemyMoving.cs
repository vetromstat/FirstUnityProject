using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMoving : MonoBehaviour
{
    [SerializeField]
    float moveSpeed;

    GameObject target = GameObject.Find("Player");

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);    
    }
}
