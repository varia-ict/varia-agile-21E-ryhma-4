using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject player;
    public Enemy enemyScript;
    public PlayerController playerControllerScript;
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
            Debug.Log("enemy hit");
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