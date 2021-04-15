using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dialog;
using System.IO;

public class ObjectController : MonoBehaviour
{
    public string m_Description;
    public string m_Action;
    public GameObject m_CoAnimatedObject;
    public GameObject m_InvisibleObject;
    public bool m_Movable;
    public bool m_ToCollectObject;
    private Rigidbody2D myScriptsRigidbody2D;

    private Animator m_Animator;
    private Animator m_CoAnimator;

    private bool m_Possible = true;
    private bool m_Interacting = false;
    private bool m_StartInteraction = false;
    private bool m_EndInteraction = false;
    
    // Start is called before the first frame update
    void Start()
    {
        myScriptsRigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        if (m_CoAnimatedObject != null)
        {
            m_CoAnimator = m_CoAnimatedObject.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (m_InvisibleObject != null && m_Movable)
        {
            float d = (GetComponent<Transform>().position - m_InvisibleObject.GetComponent<Transform>().position).magnitude;
            if (d > 50)
            {
                //Debug.Log("Clef rendue visible 1");
                m_InvisibleObject.SetActive(true);
            }
        }
    }


    public void SetEndInteraction()
    {
        m_EndInteraction = true;
        m_StartInteraction = false;
    }

    public void SetImpossible()
    {
        m_Possible = false;
    }

    public void DoAction()
    {
        //Debug.Log("Action");
        if (m_Animator != null)
        {
            m_Animator.SetBool(m_Action, true);
        }
        if (m_CoAnimatedObject != null)
        {
            m_CoAnimator.SetBool(m_Action, true);
        }

        if (gameObject.tag == "Linge")
        {
            Object.Destroy(gameObject);
        }

        if (m_Movable)
        {
            GetComponent<Rigidbody2D>().mass = 0.0001f;
        }

        if (m_ToCollectObject)
        {
            GameObject.Find("Player character").GetComponent<Inventory>().GiveItem(m_Description);
            Object.Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Pour éviter que les colliders des autres décors posent problème
        bool decorCollider = (collision.tag == "DecorCollider");
        if (!decorCollider) decorCollider = (collision.tag == "Linge");

        if (m_Possible && !decorCollider && new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude == 0 && !m_StartInteraction && !m_EndInteraction && !GameObject.Find("Player character").GetComponent<PlayerController>().IsInteracting())
        {
            m_Interacting = true;
            GameObject.Find("Player character").GetComponent<PlayerController>().SetInteracting(true);
            GetComponent<ActionManager>().PrintDescription(m_Description, m_Action);
            
            if (m_InvisibleObject != null && !m_Movable)
            {
                //Debug.Log("Clef rendue visible 2");
                m_InvisibleObject.SetActive(true);
            }
            
            m_StartInteraction = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_Possible && m_Interacting)
        {
            GetComponent<ActionManager>().HideDescription();
            m_StartInteraction = false;
            m_Interacting = false;
            m_EndInteraction = false;
            GameObject.Find("Player character").GetComponent<PlayerController>().SetInteracting(false);
        }
    }
}
