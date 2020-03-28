using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    public GameObject m_ActionBoxPrefab;

    private GameObject m_ActionBox;
    private GameObject m_DescriptionTextUI;
    private Text m_DescriptionText;

    public GameObject m_ActionButtonPrefab;

    private GameObject m_CanvasActionButton;
    private GameObject m_ActionButtonUI;
    private Button m_ActionButton;
    private Text m_ActionText;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void PrintDescription(string description, string action)
    {
        m_ActionBox = Instantiate(m_ActionBoxPrefab, transform.position, transform.rotation);

        m_DescriptionTextUI = GameObject.Find("DialogText");
        m_DescriptionText = m_DescriptionTextUI.GetComponent<Text>();
        m_DescriptionText.text = description;

        m_CanvasActionButton = Instantiate(m_ActionButtonPrefab, transform.position, transform.rotation);
        m_ActionButtonUI = GameObject.Find("ChoiceButton");
        m_ActionButton = m_ActionButtonUI.GetComponent<Button>();
        m_ActionButton.onClick.AddListener(DoAction);

        GameObject m_ActionTextUI = GameObject.Find("ChoiceText");
        m_ActionText = m_ActionTextUI.GetComponent<Text>();
        m_ActionText.text = action;
    }

    public void DoAction()
    {
        GetComponent<ObjectController>().DoAction();
        HideDescription();
    }

    public void HideDescription()
    {
        Destroy(m_CanvasActionButton);
        Destroy(m_ActionBox);
        GetComponent<ObjectController>().SetEndInteraction();
    }
}
