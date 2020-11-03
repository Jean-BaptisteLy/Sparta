using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiqueTeleport : MonoBehaviour
{
    public float picDamage = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject, 10);
    }

    void OnCollisionEnter(Collision col)
    {
    	if(col.gameObject.tag == "Player")
    	{
    		col.transform.position = new Vector3(14.9796f, 0.0f, 4.187284f);
    		col.gameObject.GetComponent<PlayerInventory>().ApplyDamage(picDamage);
    	}
    }
}
