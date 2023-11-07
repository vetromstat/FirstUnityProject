using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public class Toolbar : MonoBehaviour
{
    int Index;
    
    public RectTransform Outline;
    public void Start()
    {   
       
    }
    public void Update()
    {
        Index = GameObject.FindWithTag("Player").GetComponent<Player>().WeaponIndex;
        Outline.position = gameObject.transform.GetChild(Index).transform.position;
    }
}
