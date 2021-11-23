using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    PlayerMovement player;
    public Player_Combat combat;
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
        weaponWheelSelected = false;
        player.changeWeapon(weaponID);
    }
}
