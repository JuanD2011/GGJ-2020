﻿using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private string id = "";
    [SerializeField] protected int health = 1;
    [SerializeField] protected float speed = 1f;
    [SerializeField] protected float damagePerSecond = 0.4f;

    public string Id { get => id; private set => id = value; }

    EnemyMovement enemyMovement;

    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        enemyMovement.Movement();
    }


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

    private void OnTriggerStay(Collider other)
    {
        ISpoilable spoilableUnit = other.GetComponent<ISpoilable>();
        spoilableUnit?.Spoil(damagePerSecond);
    }

    public void OnLimit()
    {
        //TODO: DISABLE ENEMY
    }
}