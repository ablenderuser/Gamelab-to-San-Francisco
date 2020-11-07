using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public bool m_OneShot;

    public GameObject m_DialogBoxPrefab;

    private GameObject m_DialogBox;
    private GameObject m_DialogTextUI;
    private Text m_DialogText;

    public GameObject m_ChoiceButtonPrefab;

    private GameObject m_CanvasFirstChoiceButton;
    private GameObject m_CanvasSecondChoiceButton;
    private GameObject m_CanvasThirdChoiceButton;
    private GameObject m_CanvasFinishDialogButton;

    private GameObject m_FirstChoiceButtonUI;
    private GameObject m_SecondChoiceButtonUI;
    private GameObject m_ThirdChoiceButtonUI;
    private GameObject m_FinishDialogButtonUI;

    private Button m_FirstChoiceButton;
    private Button m_SecondChoiceButton;
    private Button m_ThirdChoiceButton;
    private Button m_FinishDialogButton;

    private Text m_FirstChoiceText;
    private Text m_SecondChoiceText;
    private Text m_ThirdChoiceText;
    private Text m_FinishDialogText;

    private Dialog m_Dialog;

    public Vector3 m_Decal1;
    public Vector3 m_Decal2;
    public Vector3 m_Decal3;
    public Vector3 m_DecalF;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartDialog(Dialog dialog)
    {
        m_Dialog = dialog;

        m_DialogBox = Instantiate(m_DialogBoxPrefab, transform.position, transform.rotation);

        m_DialogTextUI = GameObject.Find("InteractionBoxText");
        m_DialogText = m_DialogTextUI.GetComponent<Text>();
        m_DialogText.text = dialog.m_Sentence;

        if (m_Dialog.m_Choices[0] != "")
        {
            m_CanvasFirstChoiceButton = Instantiate(m_ChoiceButtonPrefab, transform.position, transform.rotation);
            m_FirstChoiceButtonUI = GameObject.Find("InteractionButton");
            m_FirstChoiceButtonUI.name = "FirstChoiceButton";
            m_FirstChoiceButtonUI.transform.Translate(m_Decal1);
            m_FirstChoiceButton = m_FirstChoiceButtonUI.GetComponent<Button>();
            m_FirstChoiceButton.onClick.AddListener(FirstChoiceDialog);

            GameObject m_FirstChoiceTextUI = GameObject.Find("InteractionButtonText");
            m_FirstChoiceTextUI.name = "FirstChoiceText";
            m_FirstChoiceText = m_FirstChoiceTextUI.GetComponent<Text>();
            m_FirstChoiceText.text = m_Dialog.m_Choices[0];
        }

        if (m_Dialog.m_Choices[1] != "")
        {
            m_CanvasSecondChoiceButton = Instantiate(m_ChoiceButtonPrefab, transform.position, transform.rotation);
            m_SecondChoiceButtonUI = GameObject.Find("InteractionButton");
            m_SecondChoiceButtonUI.name = "SecondChoiceButton";
            m_SecondChoiceButtonUI.transform.Translate(m_Decal2);
            m_SecondChoiceButton = m_SecondChoiceButtonUI.GetComponent<Button>();
            m_SecondChoiceButton.onClick.AddListener(SecondChoiceDialog);

            GameObject m_SecondChoiceTextUI = GameObject.Find("InteractionButtonText");
            m_SecondChoiceTextUI.name = "SecondChoiceText";
            m_SecondChoiceText = m_SecondChoiceTextUI.GetComponent<Text>();
            m_SecondChoiceText.text = m_Dialog.m_Choices[1];
        }

        if (m_Dialog.m_Choices[2] != "")
        {
            m_CanvasThirdChoiceButton = Instantiate(m_ChoiceButtonPrefab, transform.position, transform.rotation);
            m_ThirdChoiceButtonUI = GameObject.Find("InteractionButton");
            m_ThirdChoiceButtonUI.name = "ThirdChoiceButton";
            m_ThirdChoiceButtonUI.transform.Translate(m_Decal3);
            m_ThirdChoiceButton = m_ThirdChoiceButtonUI.GetComponent<Button>();
            m_ThirdChoiceButton.onClick.AddListener(ThirdChoiceDialog);

            GameObject m_ThirdChoiceTextUI = GameObject.Find("InteractionButtonText");
            m_ThirdChoiceTextUI.name = "ThirdChoiceText";
            m_ThirdChoiceText = m_ThirdChoiceTextUI.GetComponent<Text>();
            m_ThirdChoiceText.text = m_Dialog.m_Choices[2];
        }
    }

    public void FirstChoiceDialog()
    {
        if (m_Dialog.m_NextSentences.Length > 0)
        {
            m_Dialog = m_Dialog.m_NextSentences[0];
            ContinueDialog();
            HeartHealthVisual.heartHealthSystemStatic.Damage(4);
        }
        else
        {
            
            EndDialog();
        }
    }

    public void SecondChoiceDialog()
    {
        if (m_Dialog.m_NextSentences.Length > 0)
        {
            m_Dialog = m_Dialog.m_NextSentences[1];
            ContinueDialog();
        }
        else
        {
            EndDialog();
        }
    }

    public void ThirdChoiceDialog()
    {
        if (m_Dialog.m_NextSentences.Length > 0)
        {
            m_Dialog = m_Dialog.m_NextSentences[2];
            //HeartHealthVisual.heartHealthSystemStatic.Heal(4);
            ContinueDialog();
        }
        else
        {
            EndDialog();
        }
    }

    public void ContinueDialog()
    {
        Destroy(m_FirstChoiceButtonUI);
        Destroy(m_SecondChoiceButtonUI);
        Destroy(m_ThirdChoiceButtonUI);

        m_DialogText.text = m_Dialog.m_Sentence;
        if (m_Dialog.m_Choices.Length > 0)
        {
            if (m_Dialog.m_Choices[0] != "")
            {
                m_CanvasFirstChoiceButton = Instantiate(m_ChoiceButtonPrefab, transform.position, transform.rotation);
                m_FirstChoiceButtonUI = GameObject.Find("InteractionButton");
                m_FirstChoiceButtonUI.name = "FirstChoiceButton";
                m_FirstChoiceButtonUI.transform.Translate(m_Decal1);
                m_FirstChoiceButton = m_FirstChoiceButtonUI.GetComponent<Button>();
                m_FirstChoiceButton.onClick.AddListener(FirstChoiceDialog);

                GameObject m_FirstChoiceTextUI = GameObject.Find("InteractionButtonText");
                m_FirstChoiceTextUI.name = "FirstChoiceText";
                m_FirstChoiceText = m_FirstChoiceTextUI.GetComponent<Text>();
                m_FirstChoiceText.text = m_Dialog.m_Choices[0];
            }

            if (m_Dialog.m_Choices[1] != "")
            {
                m_CanvasSecondChoiceButton = Instantiate(m_ChoiceButtonPrefab, transform.position + m_Decal2, transform.rotation);
                m_SecondChoiceButtonUI = GameObject.Find("InteractionButton");
                m_SecondChoiceButtonUI.name = "SecondChoiceButton";
                m_SecondChoiceButtonUI.transform.Translate(m_Decal2);
                m_SecondChoiceButton = m_SecondChoiceButtonUI.GetComponent<Button>();
                m_SecondChoiceButton.onClick.AddListener(SecondChoiceDialog);

                GameObject m_SecondChoiceTextUI = GameObject.Find("InteractionButtonText");
                m_SecondChoiceTextUI.name = "SecondChoiceText";
                m_SecondChoiceText = m_SecondChoiceTextUI.GetComponent<Text>();
                m_SecondChoiceText.text = m_Dialog.m_Choices[1];
            }

            if (m_Dialog.m_Choices[2] != "")
            {
                m_CanvasThirdChoiceButton = Instantiate(m_ChoiceButtonPrefab, transform.position + m_Decal3, transform.rotation);
                m_ThirdChoiceButtonUI = GameObject.Find("InteractionButton");
                m_ThirdChoiceButtonUI.name = "ThirdChoiceButton";
                m_ThirdChoiceButtonUI.transform.Translate(m_Decal3);
                m_ThirdChoiceButton = m_ThirdChoiceButtonUI.GetComponent<Button>();
                m_ThirdChoiceButton.onClick.AddListener(ThirdChoiceDialog);

                GameObject m_ThirdChoiceTextUI = GameObject.Find("InteractionButtonText");
                m_ThirdChoiceTextUI.name = "ThirdChoiceText";
                m_ThirdChoiceText = m_ThirdChoiceTextUI.GetComponent<Text>();
                m_ThirdChoiceText.text = m_Dialog.m_Choices[2];
            }
        }
        else
        {
            Debug.Log("No more choices");

            m_CanvasFinishDialogButton = Instantiate(m_ChoiceButtonPrefab, transform.position, transform.rotation);
            m_FinishDialogButtonUI = GameObject.Find("InteractionButton");
            m_FinishDialogButtonUI.name = "FinishDialogButton";
            m_FinishDialogButtonUI.transform.Translate(m_DecalF);
            m_FinishDialogButton = m_FinishDialogButtonUI.GetComponent<Button>();
            m_FinishDialogButton.onClick.AddListener(EndDialog);

            GameObject m_FinishDialogTextUI = GameObject.Find("InteractionButtonText");
            m_FinishDialogTextUI.name = "ThirdChoiceText";
            m_FinishDialogText = m_FinishDialogTextUI.GetComponent<Text>();
            m_FinishDialogText.text = "Fin";

            if (m_OneShot)
            {
                Debug.Log("OneShot");
                GetComponent<NPCController>().SetImpossible();
            }
        }
    }

    public void EndDialog()
    {
        Destroy(m_CanvasFirstChoiceButton);
        Destroy(m_CanvasSecondChoiceButton);
        Destroy(m_CanvasThirdChoiceButton);
        Destroy(m_CanvasFinishDialogButton);
        Destroy(m_DialogBox);
        GetComponent<NPCController>().SetEndDialog();
    }
}
