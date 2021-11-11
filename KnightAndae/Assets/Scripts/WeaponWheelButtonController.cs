using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelButtonController : MonoBehaviour
{
    public int ID;
    private Animator anim;
    public string itemName;
    public Image selectedItem;
    WeaponWheelController wheelControl;

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
        anim.SetBool("Hover", true);
    }

    public void HoverExit()
    {
        anim.SetBool("Hover", false);
    }

    public void Clicked()
    {
        //Debug.Log("Clicked Something");
        //WeaponWheelController.weaponID = ID;
        wheelControl.updateWeapon(ID);
    }
}
