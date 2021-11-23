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
    // Start is called before the first frame update
    void Start()
    {
        combat = GameObject.FindGameObjectWithTag("PlayerHitBox").GetComponent<Player_Combat>();
        currentCheckpoint = firstCheckpoint;

        foreach (Transform child in currentCheckpoint.parent.GetChild(0))
            enemies.Add(child.gameObject);

        foreach (Transform child in currentCheckpoint.parent.GetChild(1))
            chests.Add(child.gameObject);

        lastArrowCount = combat.arrowCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCheckpoint(Transform newCheckpoint)
    {
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
        

    }

    public void respawn()
    {
        transform.position = currentCheckpoint.position;
        
        foreach (GameObject enemy in enemies)
            enemy.GetComponent<EnemyAIv2>().respawnEnemy();
        
        foreach(GameObject chest in chests)
            chest.GetComponent<Chest>().reset();
        combat.setArrow(lastArrowCount);

        foreach (GameObject loot in GameObject.FindGameObjectsWithTag("Loot"))
            Destroy(loot);

        foreach (GameObject projectile in GameObject.FindGameObjectsWithTag("Projectile"))
            Destroy(projectile);
    }

}
