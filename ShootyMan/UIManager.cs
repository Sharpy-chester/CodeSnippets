using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    Weapon.WeaponSelected currentWeapon;
    [SerializeField] Sprite[] weaponSprites;
    [SerializeField] Image weaponIcon;
    [SerializeField] Slider flameSlider;

    void Awake()
    {
        currentWeapon = Weapon.WeaponSelected.Shotgun;
    }

    private void Update()
    {
        flameSlider.value = weapon.flamethrowerOnTime / weapon.flamethrowerMaxOnTime;
    }


    //called when the button that changes the weapons is pressed
    public void WeaponBtn()
    {
        ++currentWeapon;
        if (currentWeapon >= (Weapon.WeaponSelected)System.Enum.GetValues(typeof(Weapon.WeaponSelected)).Length)
        {
            currentWeapon = 0;
        }
        weapon.ChangeWeapon(currentWeapon);
        weaponIcon.sprite = weaponSprites[(int)currentWeapon];
    }
}