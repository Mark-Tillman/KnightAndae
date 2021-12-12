using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform firstCheckpoint;
    Transform currentCheckpoint;
    List<GameObject> enemies = new List<GameObject>();
    List<GameObject> chests = new List<GameObject>();
    int lastArrowCount;
    Player_Combat combat;
    
    int checkpointNumber;
    void Start()
    {
        combat = GameObject.FindGameObjectWithTag("PlayerHitBox").GetComponent<Player_Combat>();
        currentCheckpoint = firstCheckpoint;
        checkpointNumber = int.Parse(currentCheckpoint.transform.parent.name);;
        foreach (Transform child in currentCheckpoint.parent.GetChild(0))
            enemies.Add(child.gameObject);

        foreach (Transform child in currentCheckpoint.parent.GetChild(1))
            chests.Add(child.gameObject);

        lastArrowCount = combat.arrowCount;

        if(PlayerPrefs.GetInt("loadFromSave") == 1)
        {
            loadFromSave();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCheckpoint(Transform newCheckpoint)
    {
        if (currentCheckpoint != newCheckpoint)
        {
            SoundManager.PlaySound("checkpoint");
            
        }
        newCheckpoint.gameObject.GetComponent<Animator>().SetTrigger("raiseFlag");

        foreach (Transform child in currentCheckpoint.parent.GetChild(0))
            enemies.Remove(child.gameObject);

        foreach (Transform child in currentCheckpoint.parent.GetChild(1))
            chests.Remove(child.gameObject);

        foreach (Transform child in newCheckpoint.parent.GetChild(0))
            enemies.Add(child.gameObject);

        foreach (Transform child in newCheckpoint.parent.GetChild(1))
            chests.Add(child.gameObject);

        currentCheckpoint = newCheckpoint;
        lastArrowCount = combat.arrowCount;
        checkpointNumber = int.Parse(currentCheckpoint.transform.parent.name);

    }

    public void respawn()
    {
        transform.position = currentCheckpoint.position;
        
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyAIv2>().respawnEnemy();

            if(enemy.name == "Lord Loarde")
            {
                GameObject.Find("Lord Loarde").GetComponent<LordLoarde>().reset();
            }
        }
            
        
        foreach(GameObject chest in chests)
            chest.GetComponent<Chest>().reset();

        combat.setArrow(lastArrowCount);

        foreach (GameObject loot in GameObject.FindGameObjectsWithTag("Loot"))
            Destroy(loot);

        foreach (GameObject projectile in GameObject.FindGameObjectsWithTag("Projectile"))
            Destroy(projectile);

        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public int getLastArrowCount()
    {
        return lastArrowCount;
    }
    public int getCheckpointNumber()
    {
        return checkpointNumber;
    }

    void loadFromSave()
    {
        Debug.Log("Loading From Save");
        currentCheckpoint = GameObject.FindGameObjectWithTag("CheckpointList").transform.GetChild(PlayerPrefs.GetInt("checkpointNumber") - 1).GetChild(2);
        lastArrowCount = PlayerPrefs.GetInt("arrowCount");

        //respawn();
    }

}
