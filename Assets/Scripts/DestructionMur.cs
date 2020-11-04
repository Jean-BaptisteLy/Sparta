using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionMur : MonoBehaviour
{
    public GameObject enemy;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {

    	if(col.gameObject.tag == "Player")
    	{
            enemy.gameObject.GetComponent<Rikayon>().activate();
    		Destroy(gameObject);
    	}
    }
}
