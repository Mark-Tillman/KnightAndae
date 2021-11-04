using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    PlayerMovement player;
    public Animator anim;
    private bool weaponWheelSelected = false;
    public Sprite noImage;
    public static int weaponID;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // if weapon wheel isn't active, activate it, and vice versa
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weaponWheelSelected = !weaponWheelSelected;
        }

        // animate it
        if (weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            anim.SetBool("OpenWeaponWheel", false);
        }
        
        // select the weapon
        switch (weaponID)
        {
            case 0: // no weapon selected
                break;
            case 1:
                Debug.Log("Sword");
                player.changeWeapon(weaponID);
                break;
            case 2:
                Debug.Log("Spear");
                player.changeWeapon(weaponID);
                break;
            case 3:
                Debug.Log("Bow");
                player.changeWeapon(weaponID);
                break;
            case 4:
                Debug.Log("Hammer");
                player.changeWeapon(weaponID);
                break;
            case 5:
                Debug.Log("Magic");
                player.changeWeapon(weaponID);
                break;
        }
        
    }
}
