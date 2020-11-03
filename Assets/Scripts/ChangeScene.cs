using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
    	if(col.gameObject.tag == "Player")
    	{
    		SceneManager.LoadScene("Level 2");
    	}
    }

}
