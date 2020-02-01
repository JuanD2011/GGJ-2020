using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 2;
    private Rigidbody mBody = null;

    private void Awake()
    {
        mBody = GetComponent<Rigidbody>();
    }

    public void Impulse(float _impulseForce)
    {
        mBody.AddForce(_impulseForce * transform.forward, ForceMode.Impulse);
        StartCoroutine(DissolveWithTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageableUnit = GetComponent<IDamageable>();
        if (damageableUnit != null)
        {
            damageableUnit.Damage(damage);
            gameObject.SetActive(false);
            StopAllCoroutines();
        }
    }

    /// <summary>
    /// If the bullet doesn't detect an available collision, destroy on a timer
    /// </summary>
    /// <returns></returns>
    private IEnumerator DissolveWithTime()
    {
        yield return new WaitForSeconds(4);
        gameObject.SetActive(false);
    }
}
