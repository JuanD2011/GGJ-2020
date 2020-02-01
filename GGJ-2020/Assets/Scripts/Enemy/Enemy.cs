using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private string id = "";
    [SerializeField] protected int health = 1;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float damagePerSecond = 0.4f;

    /// <summary>
    /// Take damage and check if the enemy is dead
    /// </summary>
    /// <param name="_damage"></param>
    public void Damage(int _damage)
    {
        health -= _damage;

        if (health <= 0)
        {
            OnDead();
        }
    }

    protected void OnDead()
    {
        gameObject.SetActive(false);
    }
}
