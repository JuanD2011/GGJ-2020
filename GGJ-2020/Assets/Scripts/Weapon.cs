using System.Collections;
using UnityEngine;

/// <summary>
/// An actor has this behaviour attached to an external object to serve as a weapon to shoot
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform gunMuzzlePos = null;
    [SerializeField] private Bullet mBullet = null;
    [SerializeField] private float refreshTime = 0.3f;
    [SerializeField] private float reloadTime = 2;
    [SerializeField] private float shootingForce = 6;
    [SerializeField] private int ammoCap = 4;
    [SerializeField] private bool useMultipleShots = false;
    [SerializeField] private int useMultipleCount = 3;
    [SerializeField] private float shotDispersionAngle = 5;
    private int ammo = 0;
    private bool canShoot = true;
    private Renderer mRenderer = null;
    private MaterialPropertyBlock mProperties;

    private void Awake()
    {
        mRenderer = GetComponent<Renderer>();
        mProperties = new MaterialPropertyBlock();
    }

    private void Start()
    {
        ammo = ammoCap;
    }

    /// <summary>
    /// An actor triggers the function to shoot the weapon
    /// </summary>
    public void Shoot()
    {
        try
        {
            if (ammo > 0 && canShoot)
            {
                if (!useMultipleShots) ShootRound();
                else
                    for (int i = 0; i < useMultipleCount; i++)
                        ShootRound();
                OneShotParticles ps = PoolService.Instance.GetGameObjectFromPool("BulletVFX")
                    .GetComponent<OneShotParticles>();
                ps.transform.position = gunMuzzlePos.position;
                ps.transform.rotation = gunMuzzlePos.rotation;
                ps.StartParticles();
                ammo -= 1;
                StartCoroutine(Refresh());
                if (ammo == 0) StartCoroutine(Reload());
            }

        }
        catch { Debug.LogError("Bullet in weapon is not instantiated!"); }
    }

    private void ShootRound()
    {
        Bullet newBullet = PoolService.Instance.GetGameObjectFromPool("Bullet").GetComponent<Bullet>();
        newBullet.transform.position = gunMuzzlePos.position;
        newBullet.transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, 
            Random.Range(transform.eulerAngles.y - shotDispersionAngle, transform.eulerAngles.y + shotDispersionAngle), 
            transform.eulerAngles.z));
        newBullet.Impulse(shootingForce);
    }

    /// <summary>
    /// Reload after a certain amount of time
    /// </summary>
    /// <returns></returns>
    private IEnumerator Reload()
    {
        float timer = reloadTime;
        while (timer > 0)
        {
            mRenderer.GetPropertyBlock(mProperties);
            mProperties.SetFloat("_CurrentReloadingValue", Mathf.Clamp(timer / reloadTime, 0, 1));
            mRenderer.SetPropertyBlock(mProperties);
            timer -= Time.deltaTime;
            yield return null;
        }
        ammo = ammoCap;
    }

    /// <summary>
    /// Sets a cool down for the weapon to avoid hyper infinite bullet spawning
    /// </summary>
    /// <returns></returns>
    private IEnumerator Refresh()
    {
        canShoot = false;
        yield return new WaitForSeconds(refreshTime);
        canShoot = true;
    }
}
