using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static Dialog;
//using System.IO;


public class SceneChangerObjectControl : MonoBehaviour
{
	//public string m_Description;
	//public string m_Action;
	
    private bool m_Possible = true;
    private bool m_Interacting = false;
    private bool m_StartInteraction = false;
    private bool m_EndInteraction = false;
	
	public GameObject myCanvas;
	
	private Animator m_Animator;
	private GameObject m_ActionBox;
	//public GameObject m_ActionButtonPrefab;
	//public GameObject m_ActionBoxPrefab;
	private GameObject m_DescriptionTextUI;
    private Text m_DescriptionText;
	private GameObject m_CanvasActionButton;
    private GameObject m_ActionButtonUI;
    private Button m_ActionButton;
    private Text m_ActionText;
	
	
    // Start is called before the first frame update
    void Start()
    {
		//myCanvas.enabled = false;
		myCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// Gestion collision et interaction avec le héros
    public void SetEndInteraction()
    {
        m_EndInteraction = true;
        m_StartInteraction = false;
    }

    public void SetImpossible()
    {
        m_Possible = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (m_Possible && new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude == 0 && !m_StartInteraction && !m_EndInteraction)
        {
            m_Interacting = true;
			
			/*
			Debug.Log("TriggerStay2D_1");
			m_ActionBox = Instantiate(m_ActionBoxPrefab, transform.position, transform.rotation);
			Debug.Log("TriggerStay2D_2");
			
			m_DescriptionTextUI = GameObject.Find("InteractionBoxText");
			m_DescriptionText = m_DescriptionTextUI.GetComponent<Text>();
			m_DescriptionText.text = m_Description;
			
			Debug.Log("TriggerStay2D_.3");
			m_CanvasActionButton = Instantiate(m_ActionButtonPrefab, transform.position, transform.rotation);
			Debug.Log("TriggerStay2D_4");
			m_ActionButtonUI = GameObject.Find("SceneChangerButton");
			Debug.Log("TriggerStay2D_5");
			m_ActionButton = m_ActionButtonUI.GetComponent<Button>();
			Debug.Log("TriggerStay2D_6");
			m_ActionButton.onClick.AddListener(DoAction);
			Debug.Log("TriggerStay2D_7");
			
			GameObject m_ActionTextUI = GameObject.Find("Text");
			m_ActionText = m_ActionTextUI.GetComponent<Text>();
			m_ActionText.text = m_Action;
			*/
			
			Debug.Log("TriggerStay2D_8");
			myCanvas.SetActive(true);
			Debug.Log("TriggerStay2D_9");
			
			m_StartInteraction = true;
			
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_Possible && m_Interacting)
        {
            //GetComponent<ActionManager>().HideDescription();
			myCanvas.SetActive(false);

            m_StartInteraction = false;
            m_Interacting = false;
            m_EndInteraction = false;
        }
    }
	


	public void DoAction()
    {
        Debug.Log("Action0");
        HideDescription();
    }

    public void HideDescription()
    {
        Destroy(m_CanvasActionButton);
        Destroy(m_ActionBox);
        SetEndInteraction();
    }
	
	/*
	Void OnTriggerEnter(collider :Collider){
myCanvas.enabled = true;
}
 
Void OnTriggerExit(){
myCanvas.enabled = false;
}
 
//If you want to be more specific to what gets enabled and store it all in one script you can check tags
 
Void OnTriggerEnter(collider :Collider){
     if (collider.tag =="NPCName"){
           myCanvas.enabled = true;
     }
}*/
}
