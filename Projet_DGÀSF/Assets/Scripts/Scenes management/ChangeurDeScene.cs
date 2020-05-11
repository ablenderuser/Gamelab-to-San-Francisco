using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeurDeScene : MonoBehaviour
{
	
	public string SceneACharger;
	
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
		SceneManager.LoadScene (SceneACharger); //On change de scène
	}
}
