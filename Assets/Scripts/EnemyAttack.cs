using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public int playerDamage;

    private Transform playerTransform;               // Reference to the player's position.
    private PlayerHealth playerHealth;      // Reference to the player's health.
    private NavMeshAgent nav;               // Reference to the nav mesh agent.
    private Animator anim;
    private GameObject player;

    void Awake()
    {
        // Set up the references.
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = playerTransform.GetComponent<PlayerHealth>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
            // ... set the destination of the nav mesh agent to the player.
            nav.SetDestination(playerTransform.position);
    }
    
    void PlayerHurt()
    {
        playerHealth.TakeDamage(playerDamage);
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider == player)
        {
            Debug.Log("Entering trigger");
            anim.SetBool("PlayerInRange", true);
            InvokeRepeating("PlayerHurt", 2f, 5f);
            nav.enabled = false;
        }
    }
}
