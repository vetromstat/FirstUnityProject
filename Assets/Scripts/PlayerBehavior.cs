using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private GameObject deathPanel;

    private Rigidbody rb;

    private bool isGrounded;
    public Vector3 jump;        // jumpipng vars
    public float jumpForce = 2.0f;

    private float Health = 100;
    private float Armor = 0;            //armor and health vars 
    private Slider SliderHealth;
    private Slider SliderArmor;

    public GameObject[] Weapons = new GameObject[10];
    public float[] WeaponCooldowns = new float[10];
    private float[] CopyWeaponCooldowns = new float[10];// Weapons vars
    public Image[] WeaponImages = new Image[10];
    [SerializeField]
    private GameObject Weapon;
    public int WeaponIndex = 0;
    
    
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        SliderHealth = GameObject.Find("HealthBar").GetComponent<Slider>();
        SliderArmor = GameObject.Find("ArmorBar").GetComponent<Slider>();
        Weapons[0] = Weapon;
        WeaponCooldowns.CopyTo(CopyWeaponCooldowns, 0);

    }
    void FixedUpdate()
    {
        HandleMovementInput();
        HandleRotationInput();
        HandleJump();
        


    }
    public void Update()
    {
        HandleHP();
        HandleSwitch();
        HandleShootInput();
        TimersUpdate();

    }
    void TimersUpdate()
    {
        for (int i = 0; i < 10; i++)
        {
            if (WeaponCooldowns[i] > 0)
            {
                WeaponCooldowns[i] -= Time.deltaTime;
                WeaponImages[i].fillAmount = 1 -  (WeaponCooldowns[i] /  CopyWeaponCooldowns[i]);
                Debug.Log(WeaponCooldowns[3]);
                
            }

            else
            {
                
                WeaponCooldowns[i] = 0;
                
            }
        }
        
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
           switch (WeaponIndex)
            {
                    case 0:
                    {
                        break;
                    }
                    case 1:
                    { 
                        if (WeaponCooldowns[1] == 0)
                        {
                            PlayerGun.Instance.Shoot();
                        }
                        else return;
                        break;
                    }
                    case 2:
                    {
                        if (WeaponCooldowns[2] == 0)
                        {
                            PlayerGun.Instance.Shoot();
                        }
                        else return;
                        break;
                    }
                    case 3:
                    {
                        if (WeaponCooldowns[3] == 0)
                        {
                          
                            ArmorUp(15);
                            WeaponCooldowns[3] = CopyWeaponCooldowns[3];
                           
                        }
                        else return;
                        break;
                    }
                    case 4:
                    {
                        if (WeaponCooldowns[4] == 0)
                        {
                            Heal(15);
                            WeaponCooldowns[4] = CopyWeaponCooldowns[4];
                        }
                        else return;
                        break;
                    }            
            }
        }
    }
    void OnCollisionStay(Collision other)
    {
        isGrounded = true;
        rb.velocity = Vector3.zero;
        if (other.gameObject.CompareTag("Deals damage"))
        {
            TakeDamage(0.5f);
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
            TakeDamage(10f);
        }
        else if (other.gameObject.CompareTag("Pickup"))
        {
            Heal(30);
            ArmorUp(30);
            Destroy(other.gameObject);
           
        }
    }
    void Heal(float hp)
    {
        Health += hp;
        if (Health > 100) Health = 100;
        SliderHealth.value += hp;
        
    }
    void ArmorUp(float points)
    {
        Armor += points;
        if (Armor > 100) Armor = 100;
        SliderArmor.value += points;
       
    }
    void TakeDamage(float dmg)
    {
        if (SliderArmor.value > 0)
        {
            Armor -= dmg;
            if (Armor < 0) Armor = 0;
            SliderArmor.value = Armor;
        }
        else
        {
            Health -= dmg;
            SliderHealth.value = Health;
           
        }

    }
    void HandleHP()
    {
        if (Health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    void SwitchWeapon(int Index) {
        
            Weapon.SetActive(false);
            Weapon = Weapons[Index];
            Weapon.SetActive(true);
            Debug.Log(Weapon);
        
    }
    void HandleSwitch()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0f)
        {
            if ((Input.GetAxisRaw("Mouse ScrollWheel") > 0) && ((WeaponIndex + 1) <= 9))
            {
                WeaponIndex = (WeaponIndex + 1);
                SwitchWeapon(WeaponIndex); 

                
;            }
            if ((Input.GetAxisRaw("Mouse ScrollWheel") < 0) && ((WeaponIndex - 1) >= 0))
            {
                WeaponIndex = (WeaponIndex - 1);
                SwitchWeapon(WeaponIndex);

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponIndex = 0;
            SwitchWeapon(WeaponIndex);

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponIndex = 1;
            SwitchWeapon(WeaponIndex);

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
           
            WeaponIndex = 2;
            SwitchWeapon(WeaponIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            
            WeaponIndex = 3;
            SwitchWeapon(WeaponIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            WeaponIndex = 4;
            SwitchWeapon(WeaponIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            WeaponIndex = 5;
            SwitchWeapon(WeaponIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            WeaponIndex = 6;
            SwitchWeapon(WeaponIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            WeaponIndex = 7;
            SwitchWeapon(WeaponIndex);

        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            WeaponIndex = 8;
            SwitchWeapon(WeaponIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            WeaponIndex = 9;
            SwitchWeapon(WeaponIndex);
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
