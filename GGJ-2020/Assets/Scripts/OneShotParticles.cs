using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particleSystems;
    [SerializeField] private string vfxNamePooling;

    public void StartParticles()
    {
        print("Hello");
        foreach(ParticleSystem ps in particleSystems)
            ps.Play();
        StartCoroutine(DisableOnEndingFX());
    }
}
