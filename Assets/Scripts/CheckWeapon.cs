using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWeapon : MonoBehaviour
{

	// ID de l'arme actuelle
	private int weaponID;

	//Liste de nos armes (Objets se trouvant dans la main du personnage)
	[SerializeField]
	public List<GameObject> weaponList = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount > 0)
        {
        	weaponID = gameObject.GetComponentInChildren<ItemOnObject>().item.itemID;
        }
        else {
        	weaponID = 0;
        	for(int i=0;i<weaponList.Count;i++)
        	{
        		weaponList[i].SetActive(false);
        	}
        }

        // Copier coller le bloc suivant pour chacune de vos armes
        // WeaponID correspond à l'ID de l'arme dans la BDD
        // i = x correspond à l'ID (ou index) de l'arme dans la liste

        if(weaponID == 1 && transform.childCount > 0)
        {
        	for(int i=0;i<weaponList.Count;i++)
        	{
        		if(i==0)
        		{
        			weaponList[i].SetActive(true);
        		}
        	}
        }
    }
}
