using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public GameObject player;
    private float enemySpeed = 1f;
    private float enemyRange = 10f;
    public Rigidbody enemyRb;
    public int health;
    private int currentHealth;
    public int damage;

    public Slider healthBar;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
        animator = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Damages player on collision
    private void OnTriggerEnter(Collider collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.name == "Player")
        {
            Destroy(collisionGameObject);
            Debug.Log("Game over");
        }

    }

    
    public void TakeDamage(int _damage)
    {
     //Plays animation on damage taken
        currentHealth -= _damage;
        animator.SetTrigger("isHit");

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    //Plays animation on death
    void Die()
    {
        animator.SetTrigger("Dead");
    }

    // Update is called once per frame
    void Update()
    {
        //Moves towards player
            if (player.transform.position.x < transform.position.x + enemyRange)
            {
                Vector3 enemyVector = transform.position - player.transform.position;
                transform.Translate(enemySpeed * Time.deltaTime * -enemyVector.normalized);
            }
        healthBar.value = health;
    }

    private void FixedUpdate()
    {
        if (currentHealth <= 25)
        {
            enemySpeed = 0.8f;
        }
    }

}
