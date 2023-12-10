using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleSlash : MonoBehaviour
{
    Animator Animator;
    float SlashTimer = 1.5f;
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")) Animator.Play("SwordSlash");
        if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !Animator.IsInTransition(0))
        {
            gameObject.tag = "Untagged";
        }
        else gameObject.tag = "Deals damage";
    }
}
