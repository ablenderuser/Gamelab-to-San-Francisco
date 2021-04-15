using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ActionManager : MonoBehaviour
{
    public bool m_OneShot;
    public string m_HealOrDamage;

    //private GameObject m_HealOrDamageUI;

    public GameObject m_ActionBoxPrefab;
    private Dictionary<string, GameObject> m_ActionBoxChildren;

    private GameObject m_ActionBox;
    private GameObject m_DescriptionTextUI;
    private TextMeshProUGUI m_DescriptionText;

    private string m_Action;

    public GameObject m_ActionButtonPrefab;

    private GameObject m_CanvasActionButton;
    private GameObject m_ActionButtonUI;
    private Button m_ActionButton;
    private TextMeshProUGUI m_ActionText;

    public float m_TypingSpeed;
    public Sprite m_ActionSprite;

    private bool m_alreadyHidden;

    public Vector3 m_Decal;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Affiche la fenêtre d'action
    public void PrintDescription(string description, string action)
    {
        m_Action = action;
        m_alreadyHidden = false;

        // Instantie et récupère les enfants de la fenêtre d'action
        m_ActionBox = Instantiate(m_ActionBoxPrefab, transform.position, transform.rotation);
        m_ActionBoxChildren = ChildrenComponents.GetChildren(m_ActionBox);

        // Cache les éléments liés au PNJ et au bouton suivant (inutiles mais présents dans le prefab)
        m_ActionBoxChildren["HeadBoxPNJ"].SetActive(false);
        m_ActionBoxChildren["NextButton"].SetActive(false);
        m_ActionBoxChildren["InteractionBoxTextPNJ"].SetActive(false);

        // Instancie la zone de texte et met à jour le sprite
        m_DescriptionTextUI = m_ActionBoxChildren["InteractionBoxTextPlayer"];
        m_DescriptionText = m_DescriptionTextUI.GetComponent<TextMeshProUGUI>();
        m_ActionBoxChildren["PlayerSprite"].GetComponent<Image>().sprite = m_ActionSprite;

        // Lance la coroutine d'effet d'écriture et affiche le bouton d'action à la fin
        StartCoroutine(TypingText.Type(m_DescriptionText, description, m_TypingSpeed, ActionButton));
    }

    // Affiche le bouton d'action
    private void ActionButton()
    {
        // Si la fenêtre d'action n'est pas encore cachée (évite le bug d'affichage du bouton si on s'éloigne rapidement de l'objet)
        if (!m_alreadyHidden)
        {
            // Instancie le bouton d'action
            m_CanvasActionButton = Instantiate(m_ActionButtonPrefab, transform.position, transform.rotation);
            Dictionary<string, GameObject> m_ActionButtonChildren = ChildrenComponents.GetChildren(m_CanvasActionButton);
            m_ActionButtonUI = m_ActionButtonChildren["InteractionButton"];
            m_ActionButtonUI.transform.Translate(m_Decal);
            m_ActionButton = m_ActionButtonUI.GetComponent<Button>();
            m_ActionButton.onClick.AddListener(DoAction);

            // Définit le texte de l'action
            GameObject m_ActionTextUI = m_ActionButtonChildren["InteractionButtonText"];
            m_ActionText = m_ActionTextUI.GetComponent<TextMeshProUGUI>();
            m_ActionText.text = m_Action;
        }
    }

    // Réalise l'action
    private void DoAction()
    {
        Debug.Log("Action0");
        GetComponent<ObjectController>().DoAction();
		if (m_HealOrDamage == "Heal")
		{
			HeartHealthVisual.heartHealthSystemStatic.Heal(4);
		}
        else
        {
            if (m_HealOrDamage == "Damage")
            {
                HeartHealthVisual.heartHealthSystemStatic.Damage(4);
            }
        }
        HideDescription();
        if (m_OneShot)
        {
            GetComponent<ObjectController>().SetImpossible();
        }
    }

    // Cache la fênetre d'acction
    public void HideDescription()
    {
        m_alreadyHidden = true;
        
        // Détruit le bouton et la fenêtre d'action
        Destroy(m_CanvasActionButton);
        Destroy(m_ActionBox);

        // Lance les actions liés à la fin de l'interaction
        GetComponent<ObjectController>().SetEndInteraction();
    }
}