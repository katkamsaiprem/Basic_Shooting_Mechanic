using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    

    public Transform fireingPoint;

    public GameObject fire;
    public GameObject hitPoint;
    public GameObject enemyPrefab; 
    public int numberOfEnemies = 5; 
    public float spawnRange = 10f; // Range within which to spawn enemies
    
    private int activeEnemies;
     void Start()
        {
            SpawnEnemies();
        }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
               ShootingBullets();            
        }
    }

    public void ShootingBullets()
    {
        // Define raycast
        RaycastHit hit;

        // Define the direction of the raycast
        Vector3 rayDirection = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(fireingPoint.position, rayDirection * 100, Color.green, 2.0f);

        if (Physics.Raycast(fireingPoint.position, rayDirection, out hit, 100))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            Debug.DrawRay(fireingPoint.position, rayDirection * hit.distance, Color.yellow, 2.0f);
             GameObject a= Instantiate(fire, fireingPoint.position, Quaternion.identity);
             GameObject b= Instantiate(hitPoint, hit.point, Quaternion.identity);
             
             // Play the particle systems
             ParticleSystem fireParticleSystem = a.GetComponent<ParticleSystem>();
             ParticleSystem hitPointParticleSystem = b.GetComponent<ParticleSystem>();
             
                fireParticleSystem.Play();
                hitPointParticleSystem.Play();
             
             Destroy(a,1);
             Destroy(b,1);
            
        }
        //using raycast get enemy information
        Enemy enemy = hit.transform.GetComponent<Enemy>();//take the enemy script

        if (enemy != null)
        {
            enemy.Damage(2);
        }

    }
    void SpawnEnemies()
    {
        activeEnemies = numberOfEnemies;
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0, // Assuming ground level is at y = 0
                Random.Range(-spawnRange, spawnRange)
            );

            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }
    
    public void EnemyDestroyed()
    {
        activeEnemies--;
        if (activeEnemies <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
