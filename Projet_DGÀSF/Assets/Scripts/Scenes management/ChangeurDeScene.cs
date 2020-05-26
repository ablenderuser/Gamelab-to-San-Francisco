using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeurDeScene : MonoBehaviour
{
    public string SceneACharger;
    public string compulsoryItem;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	//Called by button
	public void changeScene()
	{
        if (compulsoryItem != null)
        {
            if (GameObject.Find("Player character").GetComponent<Inventory>().InInventory(compulsoryItem))
            {
                SceneManager.LoadScene(SceneACharger); //On change de scène
            }
            else
            {
                GameObject.Find("SceneChangerObjectDialogText").GetComponent<Text>().text = "Dommage, tu réussiras mieux la prochaine fois...";
            }
        }
        else
        {
            SceneManager.LoadScene(SceneACharger); //On change de scène
        }
	}
}
