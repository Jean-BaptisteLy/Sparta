using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ChangeScene : MonoBehaviour
{
	public string sceneToLoad;
	/*
	public Object sceneToLoad;	
    void OnCollisionEnter(Collision col)
    {
    	if(col.gameObject.tag == "Player")
    	{
    		SceneManager.LoadScene(sceneToLoad.name);
    	}
    }
	*/
	public void OnTriggerEnter(Collider col)
    {
    	if(col.gameObject.tag == "Player")
    	{
    		SceneManager.LoadScene(sceneToLoad);
    	}
    }
}
