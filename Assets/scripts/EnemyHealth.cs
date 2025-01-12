using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    public int CurrentHealth;
    public bool IsDead = false;

    private Animator anim;

    // Event triggered when the enemy is destroyed
    public event Action OnEnemyDestroyed;

    void Start()
    {
        CurrentHealth = MaxHealth;
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("bullet"))
        {
            TakeDamage(50);
        }
    }

    void TakeDamage(int damage)
    {
        if (IsDead) return;

        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            IsDead = true;
            Debug.Log("Enemy Dead");
            anim.SetBool("Isdead", true);

            // Trigger the destruction event
            OnEnemyDestroyed?.Invoke();

            // Start the destruction process
            DestroyEnemy();
        }
    }

    public void DestroyEnemy()
    {
        StartCoroutine(WaitForEnemyToDie(3));
    }

    private IEnumerator WaitForEnemyToDie(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}