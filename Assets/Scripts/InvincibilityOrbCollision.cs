using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityOrbCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
    	{
    		col.gameObject.GetComponent<PlayerInventory>().SetInvincible();
    		Destroy(gameObject);
    	}
    }
}
