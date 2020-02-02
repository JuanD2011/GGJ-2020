using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotParticles : MonoBehaviour
{
    [SerializeField] private string vfxNamePooling;
    private ParticleSystem parentParticle;

    private void Awake() => parentParticle = GetComponent<ParticleSystem>();

    public void StartParticles() => parentParticle.Play();

    private void OnDisable() => PoolService.Instance.ReturnGameObjectToPools(gameObject, vfxNamePooling);
}
