using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelButtonController : MonoBehaviour
{
    public int ID;
    private Animator anim;
    public string itemName;
    public Image selectedItem;
    WeaponWheelController wheelControl;
    public Player_Combat combat;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        wheelControl = GetComponentInParent<WeaponWheelController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HoverEnter()
    {
        combat.attacking = true;
        anim.SetBool("Hover", true);
    }

    public void HoverExit()
    {
        combat.attacking = false;
        anim.SetBool("Hover", false);
    }

    public void Clicked()
    {
        
        wheelControl.updateWeapon(ID);
        combat.updateWeapon(ID);
        combat.attacking = false;
    }
}
