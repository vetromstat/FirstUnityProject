using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    private static float Health = 100;
    private static float Armor = 0;            //armor and health vars 
    private static Slider SliderHealth;
    private static Slider SliderArmor;

    public GameObject[] Weapons = new GameObject[5];
    public float[] WeaponCooldowns = new float[5];
    public  float[] WeaponAmmo = new float[5];
    private float[] CopyWeaponAmmo = new float[5];
    private float[] CopyWeaponCooldowns = new float[5];   // Weapons vars
    public Image[] WeaponImages = new Image[5];
    [SerializeField]
    private GameObject Weapon;
    public int WeaponIndex = 0;

    public int Points = 0;
    TMP_Text AmmoLabel;
    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        SliderHealth = GameObject.Find("HealthBar").GetComponent<Slider>();
        SliderArmor = GameObject.Find("ArmorBar").GetComponent<Slider>();
        AmmoLabel = GameObject.Find("AmmoLabel").GetComponent<TextMeshProUGUI>();
        Weapons[0] = Weapon;
        WeaponCooldowns.CopyTo(CopyWeaponCooldowns, 0);
        WeaponAmmo.CopyTo(CopyWeaponAmmo, 0);
        

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
        HandleAmmoLabel();
        TimersUpdate();
    }
    void TimersUpdate()
    {
        for (int i = 0; i < 5; i++)
        {
            if (WeaponCooldowns[i] > 0)
            {
                WeaponCooldowns[i] -= Time.deltaTime;
                WeaponImages[i].fillAmount = 1 -  (WeaponCooldowns[i] /  CopyWeaponCooldowns[i]);   
            }
            else
            {
                WeaponCooldowns[i] = 0;
            }
        }
        
    }
    void HandleAmmoLabel()
    {
        string txt;
        txt = ((int)Math.Round(WeaponAmmo[WeaponIndex])).ToString();
        if (txt != "-1")
            AmmoLabel.text = txt + " / " + CopyWeaponAmmo[WeaponIndex];
        else
            AmmoLabel.text = "1 / 1";
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
    Dictionary<int, WeaponBehavior> weaponBehaviors = new Dictionary<int, WeaponBehavior>
    {
        {0, new SwordWaepon()},
        {1, new PlayerGunWeapon()},
        {2, new PlayerGunWeapon()},
        {3, new ArmorUpWeapon()},
        {4, new HealWeapon()}
    };
    void HandleShootInput()
    {
        if (Input.GetButton("Fire1"))
        {

                bool CoolDown = WeaponCooldowns[WeaponIndex] == 0;
                bool AmmoAvailable = (WeaponAmmo[WeaponIndex] >= 0 | WeaponAmmo[WeaponIndex] == -1);

            if (CoolDown && AmmoAvailable)
            {
                weaponBehaviors[WeaponIndex].Shoot();
                if (WeaponAmmo[WeaponIndex] > 0) WeaponAmmo[WeaponIndex] -= Time.fixedDeltaTime;

            }
            else if (CoolDown && !AmmoAvailable)
            {
                WeaponAmmo[WeaponIndex] = CopyWeaponAmmo[WeaponIndex];
                WeaponCooldowns[WeaponIndex] = CopyWeaponCooldowns[WeaponIndex];
            }
            if (WeaponAmmo[WeaponIndex] == -1 && CoolDown)
            {
                WeaponCooldowns[WeaponIndex] = CopyWeaponCooldowns[WeaponIndex];

            }
        }
    }
    void OnCollisionStay(Collision other)
    {
        isGrounded = true;
        rb.velocity = Vector3.zero;
        if (other.gameObject.CompareTag("Enemy"))
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
        if (other.gameObject.CompareTag("Enemy"))
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
    public static void Heal(float hp)
    {
        Health += hp;
        if (Health > 100) Health = 100;
        SliderHealth.value += hp;
    }
    public static void ArmorUp(float points)
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
            Health = 100;
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
            if ((Input.GetAxisRaw("Mouse ScrollWheel") > 0) && ((WeaponIndex + 1) <= 4))
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
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponIndex = 0;
            SwitchWeapon(WeaponIndex);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponIndex = 1;
            SwitchWeapon(WeaponIndex);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
           
            WeaponIndex = 2;
            SwitchWeapon(WeaponIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            
            WeaponIndex = 3;
            SwitchWeapon(WeaponIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            WeaponIndex = 4;
            SwitchWeapon(WeaponIndex);
        }

    }
 
    void OnDisable()
    {
        deathPanel.SetActive(true);
        float highScore = PlayerPrefs.GetFloat("highScore");
        if (highScore <  Points) PlayerPrefs.SetFloat("highScore", Points);

    }

    void OnEnable()
    {
        deathPanel.SetActive(false);
    }

}
