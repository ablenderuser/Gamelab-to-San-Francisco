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

    private Animator m_Animator;
    private Animator m_CoAnimator;

    private bool m_Possible = true;
    private bool m_Interacting = false;
    private bool m_StartInteraction = false;
    private bool m_EndInteraction = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        if (m_CoAnimatedObject != null)
        {
            m_CoAnimator = m_CoAnimatedObject.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {

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
        m_Animator.SetBool(m_Action, true);
        if (m_CoAnimatedObject != null)
        {
            m_CoAnimator.SetBool(m_Action, true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (m_Possible && new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude == 0 && !m_StartInteraction && !m_EndInteraction)
        {
            m_Interacting = true;
            GetComponent<ActionManager>().PrintDescription(m_Description, m_Action);
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
        }
    }
}
