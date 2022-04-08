using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody enemyRb;
    public GameObject player;
    private float enemySpeed = 2f;
    private float enemyRange = 10f;
    private float enemyAttackCoolDown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x < transform.position.x + enemyRange)
        {
            Vector3 enemyVector = transform.position - player.transform.position;
            transform.Translate(enemySpeed * Time.deltaTime * -enemyVector.normalized);
        }
    }

    IEnumerator EnemyAttackCoolDown(float enemyAttackCoolDown) //enemy attack cooldown
    {
        yield return new WaitForSeconds(enemyAttackCoolDown);
    }

}
