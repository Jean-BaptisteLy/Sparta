﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

	// Distance entre le joueur et l'ennemi
	private float Distance;

	// Cible de l'ennemi
	public Transform Target;

	// Distance de poursuite
	public float chaseRange = 10;

	// Portée des attaques
	public float attackRange = 2.2f;

	// Cooldown des attaques
	public float attackRepeatTime = 1;
	private float attackTime;

	// Montant des dégats infligés
	public float TheDamage;

	// Agent de navigation
	private UnityEngine.AI.NavMeshAgent agent;

	// animator de l'ennemi
	//private Animation animator;
	private Animator animator;

	// Vie de l'ennemi
	public float enemyHealth;
	private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        //animator = gameObject.GetComponent<Animation>();
        animator = gameObject.GetComponent<Animator>();
        attackTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.destination = Target.position;

        if(!isDead)
        {
        	// On cherche le joueur en permanence
        	Target = GameObject.Find("Player").transform;

        	// On calcule la distance entre le joueur et l'ennemi
        	// en fonction de cette distance on effectue diverses actions
        	Distance = Vector3.Distance(Target.position, transform.position);

        	// Quand l'ennemi est loin = idle
        	if(Distance > chaseRange) idle();

        	// Quand l'ennemi est proche mais pas assez pour attaquer
        	if(Distance < chaseRange && Distance > attackRange) chase();

        	// Quand l'ennemi est assez proche pour attaquer
        	if(Distance < attackRange) attack();
        }
    }
    
    void chase()
    {
    	//animator.SetTrigger("Walk");
    	agent.destination = Target.position;
    }


    void attack()
    {
        // Empêche l'ennemi de traverser le joueur
        agent.destination = transform.position;
 
        // Si pas de cooldown
        if (Time.time > attackTime) {
            //animator.SetTrigger("Attack01");
            Target.GetComponent<PlayerInventory>().ApplyDamage(TheDamage);
            Debug.Log("L'ennemi a envoyé " + TheDamage + " points de dégâts");
            attackTime = Time.time + attackRepeatTime;
        }
    }

    void idle()
    {
        //animator.SetTrigger("Idle");
    }

    public void ApplyDamage(float TheDamage)
    {
        if (!isDead)
        {
            enemyHealth = enemyHealth - TheDamage;
            print(gameObject.name + "a subi " + TheDamage + " points de dégâts.");
 
            if(enemyHealth <= 0)
            {
                Dead();
            }
        }
    }

    public void Dead()
    {
        isDead = true;
        //animator.SetTrigger("Die");
        Destroy(transform.gameObject, 5);
        //Destroy(transform.gameObject);
    }
}
