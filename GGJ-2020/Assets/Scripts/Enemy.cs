using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 1;

    public void Damage(int _damage)
    {
        health -= _damage;

        if (health <= 0)
        {
            //On dead
        }
    }
}
