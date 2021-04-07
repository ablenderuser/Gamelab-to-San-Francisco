using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogManager : MonoBehaviour
{
    public bool m_OneShot;

    public GameObject m_DialogBoxPrefab;

    private GameObject m_DialogBox;
    private Dictionary<string, GameObject> m_DialogBoxChildren;
    private GameObject m_DialogTextPlayerUI;
    private TextMeshProUGUI m_DialogTextPlayer;
    private GameObject m_DialogTextPNJUI;
    private TextMeshProUGUI m_DialogTextPNJ;

    public GameObject m_ChoiceButtonPrefab;

    private GameObject m_CanvasFirstChoiceButton;
    private GameObject m_CanvasSecondChoiceButton;
    private GameObject m_CanvasThirdChoiceButton;
    private GameObject m_CanvasFinishDialogButton;

    private GameObject m_FirstChoiceButtonUI;
    private GameObject m_SecondChoiceButtonUI;
    private GameObject m_ThirdChoiceButtonUI;
    private GameObject m_FinishDialogButtonUI;

    private Button m_NextButton;
    private Button m_FirstChoiceButton;
    private Button m_SecondChoiceButton;
    private Button m_ThirdChoiceButton;
    private Button m_FinishDialogButton;

    private TextMeshProUGUI m_FirstChoiceText;
    private TextMeshProUGUI m_SecondChoiceText;
    private TextMeshProUGUI m_ThirdChoiceText;
    private TextMeshProUGUI m_FinishDialogText;

    private Dialog m_Dialog;

    public float m_TypingSpeed;

    public string m_Player;
    private Dictionary<string, Sprite> m_PlayerSprites;
    public Sprite m_PlayerSprite;
    public Sprite m_PlayerSadSprite;
    public Sprite m_PlayerAngrySprite;
    public Sprite m_PlayerEmbarassedSprite;

    public string m_PNJ;
    private Dictionary<string, Sprite> m_PNJSprites;
    public Sprite m_PNJSprite;
    public Sprite m_PNJSadSprite;
    public Sprite m_PNJAngrySprite;
    public Sprite m_PNJEmbarassedSprite;

    public Vector3 m_Decal1;
    public Vector3 m_Decal2;
    public Vector3 m_Decal3;
    public Vector3 m_DecalF;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerSprites = new Dictionary<string, Sprite>();
        if (m_PlayerSprite != null) m_PlayerSprites["Normal"] = m_PlayerSprite;
        if (m_PlayerSadSprite != null) m_PlayerSprites["Sad"] = m_PlayerSadSprite;
        if (m_PlayerAngrySprite != null) m_PlayerSprites["Angry"] = m_PlayerAngrySprite;
        if (m_PlayerEmbarassedSprite != null) m_PlayerSprites["Embarassed"] = m_PlayerEmbarassedSprite;

        m_PNJSprites = new Dictionary<string, Sprite>();
        if (m_PNJSprite != null) m_PNJSprites["Normal"] = m_PNJSprite;
        if (m_PNJSadSprite != null) m_PNJSprites["Sad"] = m_PNJSadSprite;
        if (m_PNJAngrySprite != null) m_PNJSprites["Angry"] = m_PNJAngrySprite;
        if (m_PNJEmbarassedSprite != null) m_PNJSprites["Embarassed"] = m_PNJEmbarassedSprite;
    }

    public void StartDialog(Dialog dialog)
    {
        m_Dialog = dialog;

        m_DialogBox = Instantiate(m_DialogBoxPrefab, transform.position, transform.rotation);
        m_DialogBoxChildren = ChildrenComponents.GetChildren(m_DialogBox);

        m_DialogBoxChildren["HeadBoxPlayer"].SetActive(false);
        m_DialogTextPlayerUI = m_DialogBoxChildren["InteractionBoxTextPlayer"];
        m_DialogTextPlayer = m_DialogTextPlayerUI.GetComponent<TextMeshProUGUI>();
        m_DialogTextPlayerUI.SetActive(false);

        m_DialogTextPNJUI = m_DialogBoxChildren["InteractionBoxTextPNJ"];

        m_DialogTextPNJ = m_DialogTextPNJUI.GetComponent<TextMeshProUGUI>();
        m_DialogBoxChildren["PNJSprite"].GetComponent<Image>().sprite = m_PNJSprites[m_Dialog.m_PNJMood];
        // m_DialogBoxChildren["PNJSprite"].GetComponent<Image>().SetNativeSize();

        m_NextButton = m_DialogBoxChildren["NextButton"].GetComponent<Button>();
        m_NextButton.onClick.AddListener(ResponseDialog);
        m_DialogBoxChildren["NextButton"].SetActive(false);

        StartCoroutine(TypingText.Type(m_DialogTextPNJ, "<u>" + m_PNJ + " :</u>\n" + m_Dialog.m_Sentence, m_TypingSpeed, m_DialogBoxChildren["NextButton"]));
    }

    private void ResponseDialog()
    {
        m_DialogBoxChildren["NextButton"].SetActive(false);
        m_DialogTextPNJUI.SetActive(false);
        m_DialogBoxChildren["HeadBoxPNJ"].SetActive(false);

        m_DialogBoxChildren["HeadBoxPlayer"].SetActive(true);
        m_DialogBoxChildren["PlayerSprite"].GetComponent<Image>().sprite = m_PlayerSprites[m_Dialog.m_PlayerMood];
        // m_DialogBoxChildren["PlayerSprite"].GetComponent<Image>().SetNativeSize();

        if (m_Dialog.m_Choices.Length == 1)
        {
            // m_DialogTextPlayer.text = "<u>" + m_Player + " :</u>\n" + m_Dialog.m_Choices[0];
            m_DialogTextPlayerUI.SetActive(true);

            if (m_Dialog.m_NextSentences.Length == 0)
            {
                StartCoroutine(TypingText.Type(m_DialogTextPlayer, "<u>" + m_Player + " :</u>\n" + m_Dialog.m_Choices[0], m_TypingSpeed, FinishButton));
            }
            else 
            {
                m_NextButton.onClick.RemoveAllListeners();
                m_NextButton.onClick.AddListener(NoChoiceDialog);
                StartCoroutine(TypingText.Type(m_DialogTextPlayer, "<u>" + m_Player + " :</u>\n" + m_Dialog.m_Choices[0], m_TypingSpeed, m_DialogBoxChildren["NextButton"]));
            }

        }
        else if (m_Dialog.m_Choices.Length > 1)
        {
           
            m_CanvasFirstChoiceButton = Instantiate(m_ChoiceButtonPrefab, transform.position, transform.rotation);
            Dictionary<string, GameObject> m_FirstChoiceChildren = ChildrenComponents.GetChildren(m_CanvasFirstChoiceButton);
            m_FirstChoiceButtonUI = m_FirstChoiceChildren["InteractionButton"];
            m_FirstChoiceButtonUI.name = "FirstChoiceButton";
            m_FirstChoiceButtonUI.transform.Translate(m_Decal1);
            m_FirstChoiceButton = m_FirstChoiceButtonUI.GetComponent<Button>();
            m_FirstChoiceButton.onClick.AddListener(FirstChoiceDialog);

            GameObject m_FirstChoiceTextUI = m_FirstChoiceChildren["InteractionButtonText"];
            m_FirstChoiceTextUI.name = "FirstChoiceText";
            m_FirstChoiceText = m_FirstChoiceTextUI.GetComponent<TextMeshProUGUI>();
            m_FirstChoiceText.text = m_Dialog.m_Choices[0];

            if (m_Dialog.m_Choices.Length > 1)
            {
                m_CanvasSecondChoiceButton = Instantiate(m_ChoiceButtonPrefab, transform.position, transform.rotation);
                Dictionary<string, GameObject> m_SecondChoiceChildren = ChildrenComponents.GetChildren(m_CanvasSecondChoiceButton);
                m_SecondChoiceButtonUI = m_SecondChoiceChildren["InteractionButton"];
                m_SecondChoiceButtonUI.name = "SecondChoiceButton";
                m_SecondChoiceButtonUI.transform.Translate(m_Decal2);
                m_SecondChoiceButton = m_SecondChoiceButtonUI.GetComponent<Button>();
                m_SecondChoiceButton.onClick.AddListener(SecondChoiceDialog);

                GameObject m_SecondChoiceTextUI = m_SecondChoiceChildren["InteractionButtonText"];
                m_SecondChoiceTextUI.name = "SecondChoiceText";
                m_SecondChoiceText = m_SecondChoiceTextUI.GetComponent<TextMeshProUGUI>();
                m_SecondChoiceText.text = m_Dialog.m_Choices[1];
            }

            if (m_Dialog.m_Choices.Length > 2)
            {
                m_CanvasThirdChoiceButton = Instantiate(m_ChoiceButtonPrefab, transform.position, transform.rotation);
                Dictionary<string, GameObject> m_ThirdChoiceChildren = ChildrenComponents.GetChildren(m_CanvasThirdChoiceButton);
                m_ThirdChoiceButtonUI = m_ThirdChoiceChildren["InteractionButton"];
                m_ThirdChoiceButtonUI.name = "ThirdChoiceButton";
                m_ThirdChoiceButtonUI.transform.Translate(m_Decal3);
                m_ThirdChoiceButton = m_ThirdChoiceButtonUI.GetComponent<Button>();
                m_ThirdChoiceButton.onClick.AddListener(ThirdChoiceDialog);

                GameObject m_ThirdChoiceTextUI = m_ThirdChoiceChildren["InteractionButtonText"];
                m_ThirdChoiceTextUI.name = "ThirdChoiceText";
                m_ThirdChoiceText = m_ThirdChoiceTextUI.GetComponent<TextMeshProUGUI>();
                m_ThirdChoiceText.text = m_Dialog.m_Choices[2];
            }
        }
        else
        {
            FinishButton();
        }
    }

    private void FinishButton()
    {
        Debug.Log("No more choices");

        m_CanvasFinishDialogButton = Instantiate(m_ChoiceButtonPrefab, transform.position, transform.rotation);
        Dictionary<string, GameObject> m_FinishChoiceChildren = ChildrenComponents.GetChildren(m_CanvasFinishDialogButton);
        m_FinishDialogButtonUI = m_FinishChoiceChildren["InteractionButton"];
        m_FinishDialogButtonUI.name = "FinishDialogButton";
        m_FinishDialogButtonUI.transform.Translate(m_DecalF);
        m_FinishDialogButton = m_FinishDialogButtonUI.GetComponent<Button>();
        m_FinishDialogButton.onClick.AddListener(EndDialog);

        GameObject m_FinishDialogTextUI = m_FinishChoiceChildren["InteractionButtonText"];
        m_FinishDialogTextUI.name = "ThirdChoiceText";
        m_FinishDialogText = m_FinishDialogTextUI.GetComponent<TextMeshProUGUI>();
        m_FinishDialogText.text = "Fin";

        if (m_OneShot)
        {
            Debug.Log("OneShot");
            GetComponent<NPCController>().SetImpossible();
        }
    }

    private void NoChoiceDialog()
    {
        if (m_Dialog.m_NextSentences.Length > 0)
        {
            m_Dialog = m_Dialog.m_NextSentences[0];
            ContinueDialog();
        }
        else 
        {
            EndDialog();
        }
    }
    private void FirstChoiceDialog()
    {
        if (m_Dialog.m_NextSentences.Length > 0)
        {
            m_Dialog = m_Dialog.m_NextSentences[0];
            ContinueDialog();
            //HeartHealthVisual.heartHealthSystemStatic.Damage(4);
        }
        else
        {
            EndDialog();
        }
    }

    private void SecondChoiceDialog()
    {
        if (m_Dialog.m_NextSentences.Length > 1)
        {
            m_Dialog = m_Dialog.m_NextSentences[1];
            ContinueDialog();
        }
        else
        {
            EndDialog();
        }
    }

    private void ThirdChoiceDialog()
    {
        if (m_Dialog.m_NextSentences.Length > 2)
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

    private void ContinueDialog()
    {
        Destroy(m_CanvasFirstChoiceButton);
        Destroy(m_CanvasSecondChoiceButton);
        Destroy(m_CanvasThirdChoiceButton);

        m_DialogBoxChildren["HeadBoxPlayer"].SetActive(false);
        m_DialogTextPlayerUI.SetActive(false);
        m_DialogBoxChildren["NextButton"].SetActive(false);

        m_DialogTextPNJUI.SetActive(true);
        m_DialogTextPNJ.text = "<u>" + m_PNJ + " :</u>\n" + m_Dialog.m_Sentence;
        m_DialogBoxChildren["HeadBoxPNJ"].SetActive(true);
        m_DialogBoxChildren["PNJSprite"].GetComponent<Image>().sprite = m_PNJSprites[m_Dialog.m_PNJMood];
        // m_DialogBoxChildren["PNJSprite"].GetComponent<Image>().SetNativeSize();

        m_NextButton.onClick.RemoveAllListeners();
        m_NextButton.onClick.AddListener(ResponseDialog);
        StartCoroutine(TypingText.Type(m_DialogTextPNJ, "<u>" + m_PNJ + " :</u>\n" + m_Dialog.m_Sentence, m_TypingSpeed, m_DialogBoxChildren["NextButton"]));
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
