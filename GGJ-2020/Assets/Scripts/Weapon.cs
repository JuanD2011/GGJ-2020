﻿using System.Collections;
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

    private Animator animator = null;

    private void Awake()
    {
        mRenderer = GetComponent<Renderer>();
        mProperties = new MaterialPropertyBlock();
        animator = GetComponentInParent<Animator>();
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
        if (!PauseManager.paused)
        {
            try
            {
                if (ammo > 0 && canShoot)
                {
                    if (!useMultipleShots) ShootRound();
                    else
                    {
                        for (int i = 0; i < useMultipleCount; i++)
                        {
                            ShootRound();
                        }
                    }
                    /*if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Armature|Shooting"))*/ animator.SetTrigger("Shoot");
                    ammo -= 1;
                    StartCoroutine(Refresh());
                    ShootVFX();
                    if (ammo == 0) StartCoroutine(Reload());

                }

            }
            catch { Debug.LogError("Bullet in weapon is not instantiated!"); } 
        }
    }

    private void ShootRound()
    {
        Bullet newBullet = PoolService.Instance.GetGameObjectFromPool("Bullet").GetComponent<Bullet>();
        newBullet.transform.position = gunMuzzlePos.position;
        newBullet.transform.rotation = Quaternion.Euler(new Vector3(gunMuzzlePos.eulerAngles.x, 
            Random.Range(gunMuzzlePos.eulerAngles.y - shotDispersionAngle, gunMuzzlePos.eulerAngles.y + shotDispersionAngle),
            gunMuzzlePos.eulerAngles.z));
        newBullet.Impulse(shootingForce);
        AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.gunShot, 1f, false);
    }

    private void ShootVFX()
    {
        OneShotParticles ps = PoolService.Instance.GetGameObjectFromPool("BulletVFX").GetComponent<OneShotParticles>();
        ps.transform.position = gunMuzzlePos.position;
        ps.transform.rotation = gunMuzzlePos.rotation;
        ps.StartParticles();
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
        AudioManager.Instance.PlaySFx(AudioManager.Instance.audioClips.gunReload, 1f, false);
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
