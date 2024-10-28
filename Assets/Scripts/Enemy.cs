
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
   
    public float stoppingDistance = 10f;
    public float retreatDistance = 5f;
    
    public int Health = 10;
    
    private Shooting shootingScript;

   /* public void Damage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            shootingScript.EnemyDestroyed();
            Destroy(this.gameObject);
        }
    }*/

     

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shootingScript = FindObjectOfType<Shooting>();
    }

    void Update()
    {
       EnemyMovement();
    }

    public void EnemyMovement()
    {
              if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
              {
                  transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
              }
              else if (Vector3.Distance(transform.position, player.position) < retreatDistance)
              {
                  transform.position = Vector3.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
              }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Bullet"))
        {
            
            Health -= 2;
            if (Health <= 0)
            {
                shootingScript.EnemyDestroyed();
                Destroy(this.gameObject);
            }
        }
        if (other.tag == "Player")
        {
            Debug.Log("Game Over");
        }
    }
    
}
