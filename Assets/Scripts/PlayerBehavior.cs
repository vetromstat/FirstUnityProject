using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using static UnityEditor.Progress;

public class TopDownCharacterMover : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private GameObject deathPanel;

    private Rigidbody rb;

    private bool isGrounded;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    private float Health = 100;
    private Slider Slider;
    
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        Slider = GameObject.FindWithTag("HB").GetComponent<Slider>();

    }
    void FixedUpdate()
    {
        HandleMovementInput();
        HandleRotationInput();
        HandleShootInput();
        HandleJump();
        HandleHP();
    }
    void HandleMovementInput()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        transform.Translate(movement* movementSpeed* Time.deltaTime, Space.World);


    }
    void HandleRotationInput()
    {
        RaycastHit _hit;
        Ray _ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray,out _hit))
        {
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
        }
    }
    void HandleShootInput()
    {
        if (Input.GetButton("Fire1"))
        {
            PlayerGun.Instance.Shoot();
        }
    }
    void OnCollisionStay(Collision other)
    {
        isGrounded = true;
        rb.velocity = Vector3.zero;
        if (other.gameObject.CompareTag("Deals damage"))
        {
            float dmg = 0.5f;
            Health -= dmg;
            Slider.value = Health;
            Debug.Log(Health);
        }
    }
    void OnCollisionExit()
    {
        isGrounded = false;
        
    }
    void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
    public void OnCollisionEnter(Collision other)
    {
      
        if (other.gameObject.CompareTag("Deals damage"))
        {
            TakeDamage(1);
        }
        else if (other.gameObject.CompareTag("Pickup"))
        {
            Heal(30);
            Destroy(other.gameObject);
           
        }
    }
    void Heal(float hp)
    {
        Health += hp;
        if (Health > 100) Health = 100;
        Slider.value += hp;
        Debug.Log(Health);
    }
    void TakeDamage(float dmg)
    {

        Health -= dmg;
        Slider.value = Health;
        Debug.Log(Health);

    }
    void HandleHP()
    {
        if (Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    void OnDisable()
    {
        deathPanel.SetActive(true);
    }

    void OnEnable()
    {
        deathPanel.SetActive(false);
    }
    
}
