using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private Transform player;
    public Transform EnemyBulletSpawn;
    public float range = 100f;
    public GameObject Muzzleflash;
    private AudioSource enemyAudioSource;
    public AudioClip EnemyFire;

    private void Start()
    {
        player = GlobalReferences.Instance.player;
        enemyAudioSource = GetComponent<AudioSource>();

    }

    public void Shoot()
    {
        Vector3 bulletDirection = GetDirection();
        RaycastHit hit;
        Muzzleflash.GetComponent<ParticleSystem>().Play();
        if (EnemyFire != null)  // Check if the sound is assigned
        {
            enemyAudioSource.priority = 128;  // Lower priority (128 is default)
            enemyAudioSource.PlayOneShot(EnemyFire, 0.2f); // Volume set to 0.5
        }
        else
        {
            Debug.LogWarning("Enemy fire sound is not assigned!");
        }

        // Raycast from the bullet spawn position towards the player
        if (Physics.Raycast(EnemyBulletSpawn.position, bulletDirection, out hit, range))
        {


            // Check if the ray hit the player and apply damage
            PlayerHealth playerHealth = hit.transform.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.PlayerTakeDamage(2);
            }
        }




    }

    private Vector3 GetDirection()
    {
        // Calculate the direction from the enemy bullet spawn position to the player position
        Vector3 direction = player.position - EnemyBulletSpawn.position;
        direction.Normalize();  // Normalize to get a unit vector
        return direction;
    }
}
