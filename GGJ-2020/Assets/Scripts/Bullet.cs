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
            mBody.velocity = Vector3.zero;
            PoolService.Instance.ReturnGameObjectToPools(this.gameObject, "Bullet");
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
        mBody.velocity = Vector3.zero;
        PoolService.Instance.ReturnGameObjectToPools(this.gameObject, "Bullet");
    }
}
