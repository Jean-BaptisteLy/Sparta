using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
	public Object sceneToLoad;
	private bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
        	isPaused = !isPaused;
    	}
    	if(isPaused)
        	Time.timeScale = 0f;
    	else
        	Time.timeScale = 1f;
    }

    void OnGUI () 
	{
	    if(isPaused)
	    {
	        // Si on clique sur le bouton alors isPaused devient faux donc le jeu reprend
	        if(GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 20, 80, 40), "Continuer"))
	        {
	            isPaused = false;
	        }
	        // Si on clique sur le bouton alors on ferme completment le jeu ou on charge la scene Menu Principal
	        // Dans le cas du bouton Quitter, il faut augmenter sa position Y pour qu'il soit plus bas.
	        if(GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 40, 80, 40), "Menu"))
	        {
	            //Application.LoadLevel(sceneToLoad.name); // Charge le menu principal
	        	SceneManager.LoadScene("MainMenu"); // Charge le menu principal
	        }
	        if(GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 100, 80, 40), "Quitter"))
	        {
	            Application.Quit(); // Ferme le jeu
	        }
    	}
	}
}
