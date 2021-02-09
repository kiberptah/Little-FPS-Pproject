﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public float maxHealth = 100;
    float currentHealth;


    void OnEnable()
    {
        EventDirector.someHeal += onGettingHealth;
        EventDirector.someAttack += onLoosingHealth;

    }
    private void OnDisable()
    {
        EventDirector.someHeal -= onGettingHealth;
        EventDirector.someAttack -= onLoosingHealth;

    }
    void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
        EventDirector.player_updateHealth(currentHealth);
    }
    public void onGettingHealth(Transform _whom, float _amount)
    {
        if (_whom == transform)
        {
            ChangeHealth(_amount);
        }
    }

    public void onLoosingHealth(Transform _who, Transform _whom, Vector3 _point, float _amount)
    {
        if (_whom == transform)
        {
            ChangeHealth(-_amount);

            if (currentHealth <= 0)
            {
                //EventDirector.someDeath?.Invoke(gameObject.transform);
                onNoHealth();
            }
        }
    }

    public void onNoHealth()
    {
        EventDirector.player_Death();
    }


    void ChangeHealth(float _amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + _amount, 0, maxHealth);
        EventDirector.player_updateHealth(currentHealth);
    }
}
