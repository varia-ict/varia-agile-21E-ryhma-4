using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody enemyRb;
    public GameObject player;
    private float enemySpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 enemyVector = transform.position - player.transform.position;
        transform.Translate(-enemyVector.normalized * enemySpeed * Time.deltaTime);
    }


}
