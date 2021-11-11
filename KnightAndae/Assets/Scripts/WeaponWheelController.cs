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
        anim.SetBool("OpenWeaponWheel", weaponWheelSelected);    
    }

    public void updateWeapon(int weaponID)
    {
        switch (weaponID)
        {
            case 0: // no weapon selected
                break;
            case 1:
                //Debug.Log("Sword");
                player.changeWeapon(weaponID, 1, 200, 0.2f); //Sword does 1 damage, 200 knockback
                weaponWheelSelected = false;
                break;
            case 2:
                //Debug.Log("Spear");
                player.changeWeapon(weaponID, 0.75f, 75, 0.3f); //Spear does 0.75 damage, 75 knockback
                weaponWheelSelected = false;
                break;
            case 3:
                //Debug.Log("Bow");
                player.changeWeapon(weaponID, 1, 200, 0.4f);
                weaponWheelSelected = false;
                break;
            case 4:
                //Debug.Log("Hammer");
                player.changeWeapon(weaponID, 0.5f, 500, 0.8f); //Hammer does 0.5 damage, 500 knockback
                weaponWheelSelected = false;
                break;
            case 5:
                //Debug.Log("Magic");
                player.changeWeapon(weaponID, 1, 200, 1f);
                weaponWheelSelected = false;
                break;
        }
    }
}
