using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform firstCheckpoint;
    Transform currentCheckpoint;
    List<GameObject> enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        currentCheckpoint = firstCheckpoint;
        foreach (Transform child in currentCheckpoint.parent.GetChild(0))
            enemies.Add(child.gameObject);
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



        foreach (Transform child in newCheckpoint.parent.GetChild(0))
            enemies.Add(child.gameObject);

        currentCheckpoint = newCheckpoint;

        

    }

    public void respawn()
    {
        transform.position = currentCheckpoint.position;
        Debug.Log(currentCheckpoint.parent.GetChild(0).tag);
        foreach (GameObject enemy in enemies)
            enemy.GetComponent<EnemyAIv2>().respawnEnemy();
    }

}
