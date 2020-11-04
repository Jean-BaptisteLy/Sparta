using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rikayon : MonoBehaviour {

    public Animator animator;

    // Distance entre le joueur et l'ennemi
	private float Distance;

	// Cible de l'ennemi
	public Transform Target;

	// Distance de poursuite
	public float chaseRange = 10;

	// Portée des attaques
	public float attackRange = 4.0f;

	// Cooldown des attaques
	public float attackRepeatTime = 1;
	private float attackTime;

	// Montant des dégats infligés
	public float TheDamage;

	// Agent de navigation
	private UnityEngine.AI.NavMeshAgent agent;

	// animator de l'ennemi
	//private Animation animator;
	//private Animator animator;

	// Vie de l'ennemi
	public float enemyHealth;
	private bool isDead = false;
    public float maxHealth;

	// Niveau 2
	public bool inofensif;
	private float timeToChangeDirection = 2.0f;
	private float timeInit = 2.0f;

    // Niveau 5
    public bool boss;
    Image hpImage;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
		attackTime = Time.time;
        hpImage = GameObject.Find("currentHP").GetComponent<Image>();
	}
	
	/*
	// Update is called once per frame
	void Update () {

        //if (Input.GetKeyDown(KeyCode.Space)) {
            animator.SetTrigger("Walk_Cycle_1");
        //}

	}
	*/

	// Update is called once per frame
    void Update()
    {
        //agent.destination = Target.position;

        if(!isDead && !inofensif)
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
        else if (!isDead && inofensif)
        {
        	//timeToChangeDirection -= Time.deltaTime;
        	animator.SetTrigger("Walk_Cycle_1");
        	/*
        	Random aleatoire = new Random();
			int entier = aleatoire.next(); //Génère un entier aléatoire positif
			int entierUnChiffre = aleatoire.next(10); //Génère un entier compris entre 0 et 9
			int mois = aleatoire.Next(1, 13); // Génère un entier compris entre 1 et 12
        	*/

        	if (timeInit < timeToChangeDirection)
        	{
        		timeInit += Time.deltaTime;
        	}
        	else
        	{
        		//Random aleatoire = new Random();
        		timeInit = 0.0f;
        		float x = Random.Range(5.0f, 16.0f);
        		agent.destination = new Vector3(x, 2.384186e-07f, 10f);
        	}
        }
    }

    void chase()
    {
    	animator.SetTrigger("Walk_Cycle_2");
    	agent.destination = Target.position;
    }


    void attack()
    {
        // Empêche l'ennemi de traverser le joueur
        agent.destination = transform.position;
 		
        // Si pas de cooldown
        if (Time.time > attackTime) {
            animator.SetTrigger("Attack_1");
            Target.GetComponent<PlayerInventory>().ApplyDamage(TheDamage);
            Debug.Log("L'ennemi a envoyé " + TheDamage + " points de dégâts");
            attackTime = Time.time + attackRepeatTime;
        }
        
    }

    void idle()
    {
        animator.SetTrigger("Rest_1");
    }

    public void ApplyDamage(float TheDamage)
    {
        if (!isDead)
        {
            enemyHealth = enemyHealth - TheDamage;
            print(gameObject.name + "a subi " + TheDamage + " points de dégâts.");
            if(boss) {
                float percentageHP = ((enemyHealth * 100) / maxHealth) / 100;
                hpImage.fillAmount = percentageHP;
            }            
            if(enemyHealth <= 0)
            {
                Dead();
            }
        }
    }

    public void Dead()
    {
        isDead = true;
        animator.SetTrigger("Die");
        //Destroy(transform.gameObject, 1);
        Destroy(transform.gameObject);
    }

}
