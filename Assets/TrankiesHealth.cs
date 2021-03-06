﻿using UnityEngine;
using UnityEngine.UI;

public class TrankiesHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;               // The amount of health each trankies starts with.
    public Slider m_Slider;                             // The slider to represent how much health the trankies currently has.
    public Image m_FillImage;                           // The image component of the slider.
    public Color m_FullHealthColor = Color.grey;       // The color the health bar will be when on full health.
    public Color m_ZeroHealthColor = Color.white;         // The color the health bar will be when on no health.
    public GameObject m_ExplosionPrefab;
    public GameObject m_HitExplosionPrefab;                    // A prefab that will be instantiated in Awake, then used whenever the tank dies.
    CapsuleCollider capsuleCollider;                     // Reference to the capsule collider.

    private AudioSource m_ExplosionAudio;               // The audio source to play when the tank explodes.
    private ParticleSystem m_ExplosionParticles;
    private ParticleSystem hitParticles;// The particle system the will play when the tank is destroyed.
    public float m_CurrentHealth;                      // How much health the tank currently has.
    public bool m_Dead;                                // Has the tank been reduced beyond zero health yet?
    

    public bool hit = false;

    private void Awake()
    {
        // Instantiate the explosion prefab and get a reference to the particle system on it.
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();

        hitParticles = Instantiate(m_HitExplosionPrefab).GetComponent<ParticleSystem>();

        // Get a reference to the audio source on the instantiated prefab.
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        // Disable the prefab so it can be activated when it's required.
        m_ExplosionParticles.gameObject.SetActive(false);

        capsuleCollider = GetComponent<CapsuleCollider>();

        
    }

    void Update()
    {
        SetHealthUI();

        if (m_CurrentHealth <= 0)
        {
            m_Dead = true;

            if (m_Dead)
                OnDeath();
        }

        
    }


    private void OnEnable()
    {
        // When the tank is enabled, reset the tank's health and whether or not it's dead.
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        // Update the health slider's value and color.
        SetHealthUI();
    }


    public void TakeDamage(int amount , Vector3 hitPoint)
    {
        

        // Reduce current health by the amount of damage done.
        m_CurrentHealth -= amount;

        // Set the position of the particle system to where the hit was sustained.
        hitParticles.transform.position = hitPoint;

        // And play the particles.
        hitParticles.Play();

        // Change the UI elements appropriately.
        //SetHealthUI();

        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (m_CurrentHealth <= 0f && m_Dead == true)
        {
            OnDeath();
        }
    }

    private void SetHealthUI()
    {
        // Set the slider's value appropriately.
        m_Slider.value = m_CurrentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }


    private void OnDeath()
    {
        // Set the flag so that this function is only called once.
        m_Dead = true;

        // Turn the collider into a trigger so shots can pass through it.
        //capsuleCollider.isTrigger = true;

        // Move the instantiated explosion prefab to the tank's position and turn it on.
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        // Play the particle system of the tank exploding.
        m_ExplosionParticles.Play();

        // Play the tank explosion sound effect.
        m_ExplosionAudio.Play();

        Destroy(GameObject.FindGameObjectWithTag("Pointer_Players"));
        
        
        // Turn the trankies off.
        this.gameObject.SetActive(false);
        
    }
}