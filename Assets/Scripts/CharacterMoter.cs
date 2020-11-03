using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoter : MonoBehaviour
{
    // Script PlayerInventory
    PlayerInventory playerInv;

	// Animations du personnage
	Animation animations;

	// Vitesses de déplacement
	public float walkSpeed;
	public float runSpeed;
	public float turnSpeed;

    // Pour attaquer
    public float attackCooldown;
    private bool isAttacking;
    private float currentCooldown;
    public float attackRange;
    public GameObject rayHit;

	// déplacements
	public string deplacementAvancer;
	public string deplacementReculer;
	public string deplacementGauche;
	public string deplacementDroite;
	//public string leftShift;

	public Vector3 jumpSpeed;
	CapsuleCollider playerCollider;

    public bool isDead = false;

    public GameObject raySpell;
    public GameObject lightningSpellGO;
    public float lightningSpellCost;
    public float lightningSpellSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        animations = gameObject.GetComponent<Animation>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
        playerInv =  gameObject.GetComponent<PlayerInventory>();
        rayHit = GameObject.Find("RayHit");
    }

    bool IsGrounded() // Si ya un sol => true
    {
    	return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), 0.08f, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
        	// Avancer
            if(Input.GetKey(deplacementAvancer) && !Input.GetKey(KeyCode.LeftShift))
            {
            	transform.Translate(0,0,walkSpeed * Time.deltaTime);
                //transform.rotation = Quaternion.identity; // permet de réinitialiser la rotation à 0
            	
                if(!isAttacking)
                {
                    animations.Play("walk");
                }
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
                if(Input.GetKeyDown(KeyCode.W))
                {
                    AttackSpell();
                }
            }

            // Sprint
            if(Input.GetKey(deplacementAvancer) && Input.GetKey(KeyCode.LeftShift))
            {
            	transform.Translate(0,0,runSpeed * Time.deltaTime);
            	animations.Play("run");
            }

            // Reculer
            if(Input.GetKey(deplacementReculer))
            {
            	transform.Translate(0,0, -(walkSpeed/2) * Time.deltaTime);
                if(!isAttacking)
                {
                    animations.Play("walk");
                }
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
                if(Input.GetKeyDown(KeyCode.W))
                {
                    AttackSpell();
                }
            }

            // La caméra suit la direction du joueur au début, ne pas cliquer sur le secondaire de la souris, sinon la caméra externe bouge autour du personnage
            // Pemet de rendre inutile les rotations suivantes juste après
            if(!Input.GetMouseButton(1))
            {
                float h = turnSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
                transform.Rotate(0,h,0);
            }
            
            
            // Rotation gauche (pas d'animation de prévu avec ce perso !)
            
            if(Input.GetKey(deplacementGauche))
            {
            	transform.Rotate(0,-turnSpeed * Time.deltaTime,0);
                // se déplacer à gauche
                //transform.Translate(-walkSpeed * Time.deltaTime,0,0);
            }

            // Rotation droite (pas d'animation de prévu avec ce perso !)
            
            if(Input.GetKey(deplacementDroite))
            {
            	transform.Rotate(0,turnSpeed * Time.deltaTime,0);
                // se déplacer à droite
                //transform.Translate(walkSpeed * Time.deltaTime,0,0);
            }
            

            
            // Si on n'avance pas et si on ne recule pas non plus
            if(!Input.GetKey(deplacementAvancer) && !Input.GetKey(deplacementReculer))
            {
                if(!isAttacking)
                {
                    animations.Play("idle");
                }
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Attack();
                }
                if(Input.GetKeyDown(KeyCode.W))
                {
                    AttackSpell();
                }
            }

            // Si on saute (GetKeyDown pour un seul clic seulement, sans enfoncer)
            if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
            	// Préparation du saut (Nécessaire en C#)
            	Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
            	v.y = jumpSpeed.y;

            	// Saut
            	gameObject.GetComponent<Rigidbody>().velocity = jumpSpeed;
            }
        }

        if(isAttacking)
        {
            currentCooldown -= Time.deltaTime;
        }

        if(currentCooldown <= 0)
        {
            currentCooldown = attackCooldown;
            isAttacking = false;
        }
    }

    public void Attack()
    {
        //isAttacking = true;
        //animations.Play("attack");
        if(!isAttacking)
        {
            animations.Play("attack");
            RaycastHit hit;
            if(Physics.Raycast(rayHit.transform.position, transform.TransformDirection(Vector3.forward), out hit, attackRange))
            {
                Debug.DrawLine(rayHit.transform.position, hit.point, Color.red);
                
                //if(hit.transform.name + " detected")
                if(hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<EnemyAI>().ApplyDamage(playerInv.currentDamage);
                    //print(hit.transform.name + " detected");
                }

                if(hit.transform.tag == "EnemyCrab")
                {
                    hit.transform.GetComponent<Rikayon>().ApplyDamage(playerInv.currentDamage);
                    //print(hit.transform.name + " detected");
                }
                
            }
            isAttacking = true;
        }
    }

    public void AttackSpell()
    {
        //isAttacking = true;
        //animations.Play("attack");
        if(!isAttacking && playerInv.currentMana >= lightningSpellCost)
        {
            animations.Play("attack");
            GameObject theSpell = Instantiate(lightningSpellGO, raySpell.transform.position, transform.rotation);
            theSpell.GetComponent<Rigidbody>().AddForce(transform.forward * lightningSpellSpeed);
            playerInv.currentMana -= lightningSpellCost;
            isAttacking = true;
        }
    }
}
