using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject player;
    private float enemySpeed = 1f;
    private float enemyRange = 10f;
    private Rigidbody enemyRb;


    // Start is called before the first frame update
    void Start()
    {

        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.name == "Player")
        {
            Destroy(collisionGameObject);
            Debug.Log("Game over");
        }

    }



    // Update is called once per frame
    void Update()
    {
            if (player.transform.position.x < transform.position.x + enemyRange)
            {
                Vector3 enemyVector = transform.position - player.transform.position;
                transform.Translate(enemySpeed * Time.deltaTime * -enemyVector.normalized);
            }
    }
}
