using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GunSwitch : MonoBehaviour
{
    [SerializeField] private int selectedWeapon = 0;

    void Start()
    {
        selectWeapon();
    }

    void Update()
    {

        int previousSelectedWeapon = selectedWeapon;

        if (CrossPlatformInputManager.GetButtonDown("gun1"))
        {
            selectedWeapon = 0;
        }
        if (CrossPlatformInputManager.GetButtonDown("gun2") && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if (CrossPlatformInputManager.GetButtonDown("gun3") && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            selectWeapon();
        }

    }

    private void selectWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}