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
    Player Pl;

    public RectTransform Outline;
    
    public void Start()
    {
        Pl = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    public void Update()
    {
        Index = Pl.WeaponIndex;    
        Outline.position = gameObject.transform.GetChild(Index).transform.position;
    }
}
