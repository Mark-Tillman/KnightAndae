using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player_Combat : MonoBehaviour
{
    public Animator animator;
    GameObject player;
    float knockBack = 200f;
    float damage = 1f;
    float cooldown;
    Vector2 oppositeDirection;
    public int weaponID;
    int tempID;
    int weaponCount;
    public bool attacking = false;
    public GameObject arrowProjectile;
    float arrowSpeed = 2000f;
    public int arrowCount = 10;
    public Text arrowText;

    void Start()
    {
        weaponCount = GameObject.FindGameObjectsWithTag("WeaponWheelButton").Length;
        weaponID = 1;
        updateWeapon(weaponID);
        player = GameObject.FindWithTag("Player");
        arrowText.text = arrowCount.ToString();
    }

    void Update()
    {

        if ((Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space)) && !attacking)
        {
            if(Input.mousePosition.y > ((float)Screen.height / Screen.width) * Input.mousePosition.x && Input.mousePosition.y > ((float)-Screen.height / Screen.width) * Input.mousePosition.x + Screen.height)
            {
                //UP
            animator.SetFloat("currentX", 0);
            animator.SetFloat("currentY", 1);
            attack();
        }
        else if(Input.mousePosition.y < ((float)Screen.height / Screen.width) * Input.mousePosition.x && Input.mousePosition.y > ((float)-Screen.height / Screen.width) * Input.mousePosition.x + Screen.height)
            {
                //RIGHT
            animator.SetFloat("currentX", 1);
            animator.SetFloat("currentY", 0);
            attack();
        }
        else if(Input.mousePosition.y > ((float)Screen.height / Screen.width) * Input.mousePosition.x && Input.mousePosition.y < ((float)-Screen.height / Screen.width) * Input.mousePosition.x + Screen.height)
            {
                //LEFT
            animator.SetFloat("currentX", -1);
            animator.SetFloat("currentY", 0);
            attack();
        }
        else if(Input.mousePosition.y < ((float)Screen.height / Screen.width) * Input.mousePosition.x && Input.mousePosition.y < ((float)-Screen.height / Screen.width) * Input.mousePosition.x + Screen.height)
            {
                //DOWN
            animator.SetFloat("currentX", 0);
            animator.SetFloat("currentY", -1);
            attack();
            }
        }

        if(Input.GetKey(KeyCode.UpArrow) && !attacking)
        {
            animator.SetFloat("currentX", 0);
            animator.SetFloat("currentY", 1);
            attack();
        }
        if(Input.GetKey(KeyCode.DownArrow) && !attacking)
        {
            animator.SetFloat("currentX", 0);
            animator.SetFloat("currentY", -1);
            attack();
        }
        if(Input.GetKey(KeyCode.RightArrow) && !attacking)
        {
            animator.SetFloat("currentX", 1);
            animator.SetFloat("currentY", 0);
            attack();
        }
        if(Input.GetKey(KeyCode.LeftArrow) && !attacking)
        {
            animator.SetFloat("currentX", -1);
            animator.SetFloat("currentY", 0);
            attack();
        }


        if (Input.GetAxis("Mouse ScrollWheel") != 0 && !attacking)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f) //Up
            {
                tempID = weaponID + 1;
                if (tempID > weaponCount)
                    tempID = 1;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f) //Down
            {
                tempID = weaponID - 1;
                if (tempID < 1)
                    tempID = weaponCount;
            }
            updateWeapon(tempID);
            player.GetComponent<PlayerMovement>().changeWeapon(tempID);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && !attacking)
        {
            tempID = 1;
            updateWeapon(tempID);
            player.GetComponent<PlayerMovement>().changeWeapon(tempID);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !attacking)
        {
            tempID = 2;
            updateWeapon(tempID);
            player.GetComponent<PlayerMovement>().changeWeapon(tempID);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !attacking && SceneManager.GetActiveScene().buildIndex > 2)
        {
            tempID = 3;
            updateWeapon(tempID);
            player.GetComponent<PlayerMovement>().changeWeapon(tempID);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && !attacking && SceneManager.GetActiveScene().buildIndex > 3)
        {
            tempID = 4;
            updateWeapon(tempID);
            player.GetComponent<PlayerMovement>().changeWeapon(tempID);
        }
    }

    void attack()
    {
        animator.SetTrigger("Attack");
            StartCoroutine(Attack());
            if (weaponID == 2)
            {
                SoundManager.PlaySound("bow");
                StartCoroutine(ShootArrow(player.transform));
            }
            else
            {
                SoundManager.PlaySound("swing");
            }
    }
    public void updateWeapon(int ID)
    {
        weaponID = ID;
        switch (weaponID)
        {
            case 0: // no weapon selected
                break;
            case 1:
                //Sword
                damage = 1.2f;
                knockBack = 150;
                cooldown = 0.3f;
                break;
            case 2:
                //Bow
                damage = 1;
                knockBack = 0;
                cooldown = 0.5f;
                break;
            case 3:
                //Spear
                damage = 1;
                knockBack = 100;
                cooldown = 0.5f;
                break;
            case 4:
                //Hammer
                damage = 0.7f;
                knockBack = 500;
                cooldown = 0.7f;
                break;
            case 5:
                //Unarmed
                damage = 0;
                knockBack = 0;
                cooldown = 0;
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !other.isTrigger)
        {
            float damageToDo = damage;
            float knockbackToDo = knockBack;
            int randomNum = Random.Range(1, 10);
            if (randomNum == 1)
            {
                //Debug.Log("CRITICAL");
                damageToDo *= 3;
                knockbackToDo *= 3;
            }
            else if(randomNum > 1 && randomNum < 5)
            {
                damageToDo *= 1.3f;
            }


            oppositeDirection = (other.transform.position - player.transform.position).normalized;
            other.gameObject.GetComponent<EnemyAIv2>().startGetAttacked(knockbackToDo, oppositeDirection, damageToDo);
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        yield return new WaitForSeconds(cooldown);
        attacking = false;
    }

    public IEnumerator ShootArrow(Transform player)
    {
        if (arrowCount > 0)
        {
            string arrowLayer = "projectileFront";
            Vector3 arrowPosition = new Vector3(player.localScale.x * 0.6f, 1, 0);
            Quaternion arrowRot = player.rotation;
            Vector2 arrowDirection = Vector2.right;
            float dirX = animator.GetFloat("currentX");
            float dirY = animator.GetFloat("currentY");

            if(dirY == 1){
                //Debug.Log("SHOOT UP");
                arrowLayer = "projectileBehind";
                if(player.localScale.x > 0)
                {
                    arrowRot = Quaternion.Euler(0,0,90);
                    arrowPosition -= new Vector3(0.6f, 0, 0);
                    arrowDirection = Vector2.up;
                }
                else
                {
                    arrowRot = Quaternion.Euler(0,0,-90);
                    arrowPosition += new Vector3(0.6f, 0, 0);
                    arrowDirection = Vector2.down;
                }
            }
            else if(dirY == -1){
                if(player.localScale.x > 0)
                {
                    arrowRot = Quaternion.Euler(0,0,-90);
                    arrowPosition -= new Vector3(0.6f, 0.2f, 0);
                    arrowDirection = Vector2.down;
                }
                else
                {
                    arrowRot = Quaternion.Euler(0,0,90);
                    arrowPosition += new Vector3(0.6f, -0.2f, 0);
                    arrowDirection = Vector2.up;
                }
            }
            GameObject arrow = Instantiate(arrowProjectile, player.position + arrowPosition, arrowRot, player) as GameObject;
            arrow.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = arrowLayer;
            yield return new WaitForSeconds(0.2f);
            arrow.GetComponent<Rigidbody2D>().simulated = true;
            arrow.GetComponent<Rigidbody2D>().AddForce(arrowDirection * arrowSpeed * player.localScale.x);
            arrow.transform.parent = null;
            arrowCount--;
            arrowText.text = arrowCount.ToString();
        }
    }

    public void addArrow(int num)
    {
        arrowCount += num;
        arrowText.text = arrowCount.ToString();
    }

    public void setArrow(int num)
    {
        arrowCount = num;
        arrowText.text = arrowCount.ToString();
    }
}
