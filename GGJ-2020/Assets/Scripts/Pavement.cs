﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pavement : MonoBehaviour, ISpoilable
{
    [SerializeField] private float initialHealthPoints = 100;
    private float healthPoints = 100;
    private Renderer mRenderer = null;
    private MaterialPropertyBlock mProperties;

    private void Awake()
    {
        mRenderer = GetComponent<Renderer>();
        mProperties = new MaterialPropertyBlock();
    }

    private void Start()
    {
        healthPoints = initialHealthPoints;
    }

    public void Spoil(float _spoilAmount)
    {
        healthPoints = Mathf.Clamp(healthPoints - _spoilAmount, 0, initialHealthPoints);
        print(healthPoints / initialHealthPoints);
        mRenderer.GetPropertyBlock(mProperties);
        mProperties.SetFloat("_CurrentReloadingValue", healthPoints / initialHealthPoints);
        mRenderer.SetPropertyBlock(mProperties);
    }

    public void PatchUp(float _amount)
    {
        healthPoints = Mathf.Clamp(healthPoints + _amount, 0, initialHealthPoints);
        mRenderer.GetPropertyBlock(mProperties);
        mProperties.SetFloat("_CurrentReloadingValue", healthPoints / initialHealthPoints);
        mRenderer.SetPropertyBlock(mProperties);
    }
}