﻿using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
    public AudioClip deathClip;                 // The sound to play when the enemy dies.
    public bool zombieDead;                                // Whether the enemy is dead.

    private Animator anim;                              // Reference to the animator.
    private AudioSource enemyAudio;                     // Reference to the audio source.
    private ParticleSystem hitParticles;                // Reference to the particle system that plays when the enemy is damaged.
    private CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
    private NavMeshAgent nav;
    private ScoreManager scoreManager;

    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        nav = GetComponent<NavMeshAgent>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();

        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        // If the enemy is dead...
        if (zombieDead)
            // ... no need to take damage so exit the function.
            return;

        // Play the hurt sound effect.
        enemyAudio.Play();

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= amount;

        // Set the position of the particle system to where the hit was sustained.
        hitParticles.transform.position = hitPoint;

        // And play the particles.
        hitParticles.Play();

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is dead.
            Death();
        }
    }


    public void Death()
    {
        // The enemy is dead.
        zombieDead = true;
        nav.enabled = false;

        // Tell the animator that the enemy is dead.
        anim.SetTrigger("ZombieDead");

        // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        Destroy(gameObject, 3f);
        scoreManager.Score(scoreValue);
    }
}