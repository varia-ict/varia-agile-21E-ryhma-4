using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject player;
    private Enemy enemyScript;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyScript.enemyHp -= 25;

            if (enemyScript.enemyHp <= 0)
            {
                Destroy(other.gameObject);
            }

        }

        if (other.gameObject.CompareTag("Player"))
        {
            playerControllerScript.playerHp -= 25;
        }
    }
}