using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int PlayerMaxHealth;
    public int PlayerCurrentHealth;
    public bool IsDead = false;
    public healthBar hb;

    // private Animator anim;

    private void Start()
    {
        PlayerCurrentHealth = PlayerMaxHealth;

    }

    


    public void PlayerTakeDamage( int EnemyDamage)
    {
        
        if (IsDead) return;

        PlayerCurrentHealth -= EnemyDamage;
        hb.setHealth(PlayerCurrentHealth);

        if (PlayerCurrentHealth <= 0)
        {
            IsDead = true;
            SceneManager.LoadScene("Death");
        }
    }
}