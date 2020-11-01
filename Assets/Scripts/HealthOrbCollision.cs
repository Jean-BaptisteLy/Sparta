﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthOrbCollision : MonoBehaviour
{
	public float hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
    	{
    		col.gameObject.GetComponent<PlayerInventory>().ApplyHeal(hp);
    		Destroy(gameObject);
    	}
    }
}
