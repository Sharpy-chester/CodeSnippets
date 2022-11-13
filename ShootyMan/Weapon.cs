using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject[] weapons;
    
    [SerializeField] Joystick joystick;
    [SerializeField] WeaponSelected weaponSelected;
    public float weaponCooldown;
    [SerializeField] float weaponCurrentCooldown;

    [Header("Shotgun")]
    [SerializeField] GameObject pelletPrefab;
    [SerializeField] int pelletAmt;
    [SerializeField] float pelletRange;
    [SerializeField] float spread;
    public float shotgunCooldown;
    [SerializeField] float pelletSpeed;

    [Header("Blade")]
    [SerializeField] GameObject bladePrefab;
    public float bladeCooldown;
    [SerializeField] float bladeSpeed;
    [SerializeField] float rotationSpeed;

    [Header("Flamethrower")]
    [SerializeField] GameObject flameParticleSys;
    public float flamethrowerCooldown;
    public float flamethrowerOnTime;
    public float flamethrowerMaxOnTime;
    

    [Header("Grenade")]
    [SerializeField] GameObject grenadePrefab;
    [SerializeField] float grenadeSpeed;
    [SerializeField] float grenadeCooldown;


    public enum WeaponSelected
    {
        Shotgun,
        Blade,
        Flamethrower,
        Grenade
    }

    public void ChangeWeapon(WeaponSelected weapon)
    {
        weaponSelected = weapon;
        switch (weapon)
        {
            case WeaponSelected.Shotgun:
                weaponCooldown = shotgunCooldown;
                break;
            case WeaponSelected.Blade:
                weaponCooldown = bladeCooldown;
                break;
            case WeaponSelected.Flamethrower:
                weaponCooldown = 0;                                         //set weapon cooldown to 0
                break;
            case WeaponSelected.Grenade:
                weaponCooldown = grenadeCooldown;
                break;
            default:
                break;
        }
    }

    void Awake()
    {
        weaponCurrentCooldown = 0;
        weaponCooldown = shotgunCooldown;
        StopFlamethrower();
    }

    void LateUpdate() //made this late update as before it would sometimes shoot in the wrong direction if changing direction abruptly
    {
        if (joystick.Horizontal == 0 && joystick.Vertical == 0 && weaponSelected == WeaponSelected.Flamethrower)
        {
            StopFlamethrower();
            
        }
        if (weaponCooldown >= weaponCurrentCooldown)
        {
            weaponCurrentCooldown += Time.deltaTime;
        }
        //if the shooting joysticks are being used AND if the cooldowns over
        if ((joystick.Horizontal != 0 || joystick.Vertical != 0) && weaponCurrentCooldown >= weaponCooldown) 
        {
            switch (weaponSelected)
            {
                case WeaponSelected.Shotgun:
                    FireShotgun();
                    break;
                case WeaponSelected.Blade:
                    FireBlade();
                    break;
                case WeaponSelected.Flamethrower:
                    FireFlamethrower();
                    break;
                case WeaponSelected.Grenade:
                    FireGrenade();
                    break;
            }
            weaponCurrentCooldown = 0;
        }

        
    }

    void FireShotgun()
    {
        for (int i = 0; i < pelletAmt; i++)
        {
            float inaccuracy = Random.Range(-spread, spread);
            GameObject bullet = Instantiate(pelletPrefab, transform);
            bullet.transform.parent = null;
            bullet.transform.eulerAngles = new Vector3(bullet.transform.eulerAngles.x, bullet.transform.eulerAngles.y, bullet.transform.eulerAngles.z + inaccuracy);
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * pelletSpeed, ForceMode2D.Impulse);
        }
    }

    void FireBlade()
    {
        GameObject blade = Instantiate(bladePrefab, transform);
        blade.transform.parent = null;
        blade.GetComponent<Rigidbody2D>().AddForce(blade.transform.right * bladeSpeed, ForceMode2D.Impulse);
        blade.GetComponent<Rigidbody2D>().AddTorque(rotationSpeed);
    }

    void FireFlamethrower()
    {
        weaponCooldown = 0;
        if (!flameParticleSys.GetComponent<ParticleSystem>().isPlaying)
        {
            flameParticleSys.GetComponent<ParticleSystem>().Play();
        }
        flamethrowerOnTime += Time.deltaTime;
        if (flamethrowerOnTime > flamethrowerMaxOnTime)
        {
            StopFlamethrower();
            weaponCooldown = flamethrowerCooldown;
        }
    }

    void StopFlamethrower()
    {
        print("stop");
        flameParticleSys.GetComponent<ParticleSystem>().Stop();
        flamethrowerOnTime = 0;
    }

    void FireGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform);
        grenade.transform.parent = null;
        grenade.GetComponent<Rigidbody2D>().AddForce(grenade.transform.right * grenadeSpeed, ForceMode2D.Impulse);
    }
}