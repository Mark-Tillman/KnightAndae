using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    Vector3 localScale;
    EnemyAIv2 enemyAi;

    // Start is called before the first frame update
    void Start()
    {
        enemyAi = GetComponentInParent<EnemyAIv2>();
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        localScale.x = (float)enemyAi.totalHealth/enemyAi.maxHealth;
        transform.localScale = localScale;
    }
}
