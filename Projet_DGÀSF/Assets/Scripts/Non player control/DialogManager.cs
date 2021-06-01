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
    private GameObject m_CanvasFourthChoiceButton;
    private GameObject m_CanvasFinishDialogButton;

    private GameObject m_FirstChoiceButtonUI;
    private GameObject m_SecondChoiceButtonUI;
    private GameObject m_ThirdChoiceButtonUI;
    private GameObject m_FourthChoiceButtonUI;
    private GameObject m_FinishDialogButtonUI;

    private Button m_NextButton;
    private Button m_FirstChoiceButton;
    private Button m_SecondChoiceButton;
    private Button m_ThirdChoiceButton;
    private Button m_FourthChoiceButton;
    private Button m_FinishDialogButton;

    private TextMeshProUGUI m_FirstChoiceText;
    private TextMeshProUGUI m_SecondChoiceText;
    private TextMeshProUGUI m_ThirdChoiceText;
    private TextMeshProUGUI m_FourthChoiceText;
    private TextMeshProUGUI m_FinishDialogText;

    private Dialog m_Dialog;
    public float m_TypingSpeed;

    public string m_Player;
    private Dictionary<string, Sprite> m_PlayerSprites;
    public Sprite m_PlayerSprite;
    public Sprite m_PlayerHappySprite;
    public Sprite m_PlayerSadSprite;
    public Sprite m_PlayerAngrySprite;
    public Sprite m_PlayerEmbarrassedSprite;
    public Sprite m_PlayerExhaustedSprite;
    public Sprite m_PlayerBoredSprite;
    public Sprite m_PlayerCunningSprite;
    public Sprite m_PlayerSurprisedSprite;


    public string[] m_PNJs;
    private Dictionary<string, Sprite>[] m_PNJsSprites;
    public Sprite[] m_PNJsSprite;
    public Sprite[] m_PNJsHappySprite;
    public Sprite[] m_PNJsSadSprite;
    public Sprite[] m_PNJsAngrySprite;
    public Sprite[] m_PNJsEmbarrassedSprite;
    public Sprite[] m_PNJsExhaustedSprite;
    public Sprite[] m_PNJsBoredSprite;
    public Sprite[] m_PNJsCunningSprite;
    public Sprite[] m_PNJsSurprisedSprite;

    public Vector3 m_Decal1;
    public Vector3 m_Decal2;
    public Vector3 m_Decal3;
    public Vector3 m_Decal4;
    public Vector3 m_DecalF;

    private float m_ButtonWidth;
    private float m_ButtonTextWidth;

    // Start is called before the first frame update
    void Start()
    {
        // Dictionnaire de Sprites pour le joueur
        m_PlayerSprites = new Dictionary<string, Sprite>();
        if (m_PlayerSprite != null) m_PlayerSprites["Normal"] = m_PlayerSprite;
        if (m_PlayerHappySprite != null) m_PlayerSprites["Happy"] = m_PlayerHappySprite;
        if (m_PlayerSadSprite != null) m_PlayerSprites["Sad"] = m_PlayerSadSprite;
        if (m_PlayerAngrySprite != null) m_PlayerSprites["Angry"] = m_PlayerAngrySprite;
        if (m_PlayerEmbarrassedSprite != null) m_PlayerSprites["Embarrassed"] = m_PlayerEmbarrassedSprite;
        if (m_PlayerExhaustedSprite != null) m_PlayerSprites["Exhausted"] = m_PlayerExhaustedSprite;
        if (m_PlayerBoredSprite != null) m_PlayerSprites["Bored"] = m_PlayerBoredSprite;
        if (m_PlayerCunningSprite != null) m_PlayerSprites["Cunning"] = m_PlayerCunningSprite;
        if (m_PlayerSurprisedSprite != null) m_PlayerSprites["Surprised"] = m_PlayerSurprisedSprite;

        // Dictionnaire de Sprites pour le PNJ
        m_PNJsSprites = new Dictionary<string, Sprite>[m_PNJs.Length];
        for (int i = 0 ; i < m_PNJsSprites.Length ; i++) {
            m_PNJsSprites[i] = new Dictionary<string, Sprite>();
            if (i < m_PNJsSprite.Length && m_PNJsSprite[i] != null) m_PNJsSprites[i]["Normal"] = m_PNJsSprite[i];
            if (i < m_PNJsHappySprite.Length && m_PNJsHappySprite[i] != null) m_PNJsSprites[i]["Happy"] = m_PNJsHappySprite[i];
            if (i < m_PNJsSadSprite.Length && m_PNJsSadSprite[i] != null) m_PNJsSprites[i]["Sad"] = m_PNJsSadSprite[i];
            if (i < m_PNJsAngrySprite.Length && m_PNJsAngrySprite[i] != null) m_PNJsSprites[i]["Angry"] = m_PNJsAngrySprite[i];
            if (i < m_PNJsEmbarrassedSprite.Length && m_PNJsEmbarrassedSprite[i] != null) m_PNJsSprites[i]["Embarrassed"] = m_PNJsEmbarrassedSprite[i];
            if (i < m_PNJsExhaustedSprite.Length && m_PNJsExhaustedSprite[i] != null) m_PNJsSprites[i]["Exhausted"] = m_PNJsExhaustedSprite[i];
            if (i < m_PNJsBoredSprite.Length && m_PNJsBoredSprite[i] != null) m_PNJsSprites[i]["Bored"] = m_PNJsBoredSprite[i];
            if (i < m_PNJsCunningSprite.Length && m_PNJsCunningSprite[i] != null) m_PNJsSprites[i]["Cunning"] = m_PNJsCunningSprite[i];
            if (i < m_PNJsSurprisedSprite.Length && m_PNJsSurprisedSprite[i] != null) m_PNJsSprites[i]["Surprised"] = m_PNJsSurprisedSprite[i];
        }
    }

    // Lance le dialogue
    public void StartDialog(Dialog dialog)
    {
        m_Dialog = dialog;

        m_DialogBox = Instantiate(m_DialogBoxPrefab, transform.position, transform.rotation);
        m_DialogBoxChildren = ChildrenComponents.GetChildren(m_DialogBox);

        // Instancie et cache les éléments liés au joueur
        m_DialogBoxChildren["HeadBoxPlayer"].SetActive(false);
        m_DialogTextPlayerUI = m_DialogBoxChildren["InteractionBoxTextPlayer"];
        m_DialogTextPlayer = m_DialogTextPlayerUI.GetComponent<TextMeshProUGUI>();
        m_DialogTextPlayerUI.SetActive(false);

        // Instancie les éléments liés au PNJ et met à jour son sprite
        m_DialogTextPNJUI = m_DialogBoxChildren["InteractionBoxTextPNJ"];
        m_DialogTextPNJ = m_DialogTextPNJUI.GetComponent<TextMeshProUGUI>();
        m_DialogBoxChildren["PNJSprite"].GetComponent<Image>().sprite = m_PNJsSprites[m_Dialog.m_PNJIndex][m_Dialog.m_PNJMood];

        // Instancie et cache les éléments liés au bouton suivant
        m_NextButton = m_DialogBoxChildren["NextButton"].GetComponent<Button>();
        m_NextButton.onClick.AddListener(() => ResponseDialog());
        m_DialogBoxChildren["NextButton"].SetActive(false);

        // Lance la coroutine pour l'effet d'écriture et affiche le bouton suivant à la fin
        StartCoroutine(TypingText.Type(m_DialogTextPNJ, "<u>" + m_PNJs[m_Dialog.m_PNJIndex] + " :</u>\n" + m_Dialog.m_Sentence, m_TypingSpeed, m_DialogBoxChildren["NextButton"]));
    }

    // Gère l'affichage de la réponse du joueur (texte ou boutons de choix)
    private void ResponseDialog()
    {
        // Si le joueur réponds
        if (! string.IsNullOrEmpty(m_Dialog.m_PlayerMood))
        {
            // Cache les éléments liés au bouton suivant et au PNJ
            m_DialogBoxChildren["NextButton"].SetActive(false);
            m_DialogTextPNJUI.SetActive(false);
            m_DialogBoxChildren["HeadBoxPNJ"].SetActive(false);

            // Affiche le sprite joueur et le met à jour
            m_DialogBoxChildren["HeadBoxPlayer"].SetActive(true);
            m_DialogBoxChildren["PlayerSprite"].GetComponent<Image>().sprite = m_PlayerSprites[m_Dialog.m_PlayerMood];

            // Si il y a qu'un réponse du joueur possible
            if (m_Dialog.m_Choices.Length == 1)
            {
                // Affiche la zone de texte du joueur
                m_DialogTextPlayerUI.SetActive(true);

                // Si il n'y a pas de réponse du PNJ, lance la coroutine pour l'effet d'écriture et affiche le bouton Fin à la fin
                if (m_Dialog.m_NextSentences.Length == 0)
                {
                    StartCoroutine(TypingText.Type(m_DialogTextPlayer, "<u>" + m_Player + " :</u>\n" + m_Dialog.m_Choices[0], m_TypingSpeed, FinishButton));
                }
                // Sinon, lance la coroutine et affiche le bouton suivant à la fin
                else 
                {
                    m_NextButton.onClick.RemoveAllListeners();
                    m_NextButton.onClick.AddListener(() => ChoiceDialog(-1));
                    StartCoroutine(TypingText.Type(m_DialogTextPlayer, "<u>" + m_Player + " :</u>\n" + m_Dialog.m_Choices[0], m_TypingSpeed, m_DialogBoxChildren["NextButton"]));
                }

            }
            // Si il y a un choix de réponse, ajoute les boutons pour chaque choix
            else if (m_Dialog.m_Choices.Length > 1)
            {
            // Instancie le premier bouton
                m_CanvasFirstChoiceButton = Instantiate(m_ChoiceButtonPrefab, transform.position, transform.rotation);
                Dictionary<string, GameObject> m_FirstChoiceChildren = ChildrenComponents.GetChildren(m_CanvasFirstChoiceButton);
                m_FirstChoiceButtonUI = m_FirstChoiceChildren["InteractionButton"];
                m_FirstChoiceButtonUI.name = "FirstChoiceButton";
                m_FirstChoiceButtonUI.transform.Translate(m_Decal1);
                m_FirstChoiceButton = m_FirstChoiceButtonUI.GetComponent<Button>();
                m_FirstChoiceButton.onClick.AddListener(() => ChoiceDialog(1));

                // Défini le texte du premier bouton
                GameObject m_FirstChoiceTextUI = m_FirstChoiceChildren["InteractionButtonText"];
                m_FirstChoiceTextUI.name = "FirstChoiceText";
                m_FirstChoiceText = m_FirstChoiceTextUI.GetComponent<TextMeshProUGUI>();
                m_FirstChoiceText.text = m_Dialog.m_Choices[0];

                // Modification de la taille des boutons pour accueillir 3 ou 4 boutons
                if (m_Dialog.m_Choices.Length > 2)
                {
                    m_ButtonWidth = m_FirstChoiceButtonUI.GetComponent<RectTransform>().rect.width * 0.492f;
                    m_ButtonTextWidth = m_FirstChoiceTextUI.GetComponent<RectTransform>().rect.width * 0.492f;

                    m_FirstChoiceButtonUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_ButtonWidth);
                    m_FirstChoiceTextUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_ButtonTextWidth);
                }

                // Instancie le second bouton
                m_CanvasSecondChoiceButton = Instantiate(m_ChoiceButtonPrefab, transform.position, transform.rotation);
                Dictionary<string, GameObject> m_SecondChoiceChildren = ChildrenComponents.GetChildren(m_CanvasSecondChoiceButton);
                m_SecondChoiceButtonUI = m_SecondChoiceChildren["InteractionButton"];
                m_SecondChoiceButtonUI.name = "SecondChoiceButton";
                m_SecondChoiceButtonUI.transform.Translate(m_Decal2);
                m_SecondChoiceButton = m_SecondChoiceButtonUI.GetComponent<Button>();
                m_SecondChoiceButton.onClick.AddListener(() => ChoiceDialog(2));

                // Défini le texte du second bouton
                GameObject m_SecondChoiceTextUI = m_SecondChoiceChildren["InteractionButtonText"];
                m_SecondChoiceTextUI.name = "SecondChoiceText";
                m_SecondChoiceText = m_SecondChoiceTextUI.GetComponent<TextMeshProUGUI>();
                m_SecondChoiceText.text = m_Dialog.m_Choices[1];

                // Modification de la taille des boutons pour accueillir 3 ou 4 boutons
                if (m_Dialog.m_Choices.Length > 2)
                {
                    m_SecondChoiceButtonUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_ButtonWidth);
                    m_SecondChoiceTextUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_ButtonTextWidth);
                }

                // S'il y a un troisième choix, instancie le troisième bouton et défini son texte
                if (m_Dialog.m_Choices.Length > 2)
                {
                    m_CanvasThirdChoiceButton = Instantiate(m_ChoiceButtonPrefab, transform.position, transform.rotation);
                    Dictionary<string, GameObject> m_ThirdChoiceChildren = ChildrenComponents.GetChildren(m_CanvasThirdChoiceButton);
                    m_ThirdChoiceButtonUI = m_ThirdChoiceChildren["InteractionButton"];
                    m_ThirdChoiceButtonUI.name = "ThirdChoiceButton";
                    m_ThirdChoiceButtonUI.transform.Translate(m_Decal3);
                    m_ThirdChoiceButton = m_ThirdChoiceButtonUI.GetComponent<Button>();
                    m_ThirdChoiceButton.onClick.AddListener(() => ChoiceDialog(3));

                    GameObject m_ThirdChoiceTextUI = m_ThirdChoiceChildren["InteractionButtonText"];
                    m_ThirdChoiceTextUI.name = "ThirdChoiceText";
                    m_ThirdChoiceText = m_ThirdChoiceTextUI.GetComponent<TextMeshProUGUI>();
                    m_ThirdChoiceText.text = m_Dialog.m_Choices[2];

                    m_ThirdChoiceButtonUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_ButtonWidth);
                    m_ThirdChoiceTextUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_ButtonTextWidth);
                }

                // S'il y a un quatrième choix, instancie le quatrième bouton et défini son texte
                if (m_Dialog.m_Choices.Length > 3)
                {
                    m_CanvasFourthChoiceButton = Instantiate(m_ChoiceButtonPrefab, transform.position, transform.rotation);
                    Dictionary<string, GameObject> m_FourthChoiceChildren = ChildrenComponents.GetChildren(m_CanvasFourthChoiceButton);
                    m_FourthChoiceButtonUI = m_FourthChoiceChildren["InteractionButton"];
                    m_FourthChoiceButtonUI.name = "FourthChoiceButton";
                    m_FourthChoiceButtonUI.transform.Translate(m_Decal4);
                    m_FourthChoiceButton = m_FourthChoiceButtonUI.GetComponent<Button>();
                    m_FourthChoiceButton.onClick.AddListener(() => ChoiceDialog(4));

                    GameObject m_FourthChoiceTextUI = m_FourthChoiceChildren["InteractionButtonText"];
                    m_FourthChoiceTextUI.name = "FourthChoiceText";
                    m_FourthChoiceText = m_FourthChoiceTextUI.GetComponent<TextMeshProUGUI>();
                    m_FourthChoiceText.text = m_Dialog.m_Choices[3];

                    m_FourthChoiceButtonUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_ButtonWidth);
                    m_FourthChoiceTextUI.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_ButtonTextWidth);
                }
            }
            // Sinon, affiche le bouton de fin
            else
            {
                FinishButton();
            }
        }
        // Sinon on enchaîne avec la prochaine phrase du PNJ ou on ferme le dialogue
        else
        {
            ChoiceDialog(-1);
        }
    }

    // Affiche le bouton de fin
    private void FinishButton()
    {
        Debug.Log("No more choices");

        // Instancie le bouton de fin
        m_CanvasFinishDialogButton = Instantiate(m_ChoiceButtonPrefab, transform.position, transform.rotation);
        Dictionary<string, GameObject> m_FinishChoiceChildren = ChildrenComponents.GetChildren(m_CanvasFinishDialogButton);
        m_FinishDialogButtonUI = m_FinishChoiceChildren["InteractionButton"];
        m_FinishDialogButtonUI.name = "FinishDialogButton";
        m_FinishDialogButtonUI.transform.Translate(m_DecalF);
        m_FinishDialogButton = m_FinishDialogButtonUI.GetComponent<Button>();
        m_FinishDialogButton.onClick.AddListener(() => EndDialog());

        // Défini son texte
        GameObject m_FinishDialogTextUI = m_FinishChoiceChildren["InteractionButtonText"];
        m_FinishDialogTextUI.name = "ThirdChoiceText";
        m_FinishDialogText = m_FinishDialogTextUI.GetComponent<TextMeshProUGUI>();
        m_FinishDialogText.text = "Fin";

        // Si le dialogue est OneShot, rend d'autres interaction avec le PNJ impossibles
        if (m_OneShot)
        {
            Debug.Log("OneShot");
            GetComponent<NPCController>().SetImpossible();
        }
    }

    // Gère le choix du joueur (ou affiche la suite s'il n'y a pas de choix)
    private void ChoiceDialog(int choice) {
        // No choice
        if (choice == -1)
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
        // Choice
        else
        {
            if (m_Dialog.m_NextSentences.Length > choice - 1)
            {
                m_Dialog = m_Dialog.m_NextSentences[choice - 1];
                ContinueDialog();
            }
            else
            {
                EndDialog();
            }
        }
    }

    // Poursuit le dialogue du PNJ
    private void ContinueDialog()
    {
        // Destruction des boutons de choix
        Destroy(m_CanvasFirstChoiceButton);
        Destroy(m_CanvasSecondChoiceButton);
        Destroy(m_CanvasThirdChoiceButton);
        Destroy(m_CanvasFourthChoiceButton);

        // Cache les éléments liés au joueur
        m_DialogBoxChildren["HeadBoxPlayer"].SetActive(false);
        m_DialogTextPlayerUI.SetActive(false);
        m_DialogBoxChildren["NextButton"].SetActive(false);

        // Affiche les éléments liés au PNJ et met à jour son sprite
        m_DialogTextPNJUI.SetActive(true);
        m_DialogBoxChildren["HeadBoxPNJ"].SetActive(true);
        m_DialogBoxChildren["PNJSprite"].GetComponent<Image>().sprite = m_PNJsSprites[m_Dialog.m_PNJIndex][m_Dialog.m_PNJMood];

        // Réinitialise l'écouteur de bouton suivant
        m_NextButton.onClick.RemoveAllListeners();
        m_NextButton.onClick.AddListener(() => ResponseDialog());

        // Lance la coroutine d'effet d'écriture et affiche le bouton suivant à la fin
        StartCoroutine(TypingText.Type(m_DialogTextPNJ, "<u>" + m_PNJs[m_Dialog.m_PNJIndex] + " :</u>\n" + m_Dialog.m_Sentence, m_TypingSpeed, m_DialogBoxChildren["NextButton"]));
    }

    public void EndDialog()
    {
        // Détruit les boutons de choix et la boite de dialogue
        Destroy(m_CanvasFirstChoiceButton);
        Destroy(m_CanvasSecondChoiceButton);
        Destroy(m_CanvasThirdChoiceButton);
        Destroy(m_CanvasFourthChoiceButton);
        Destroy(m_CanvasFinishDialogButton);
        Destroy(m_DialogBox);

        // Lance les actions du NPC liées à la fin du dialogue
        GetComponent<NPCController>().SetEndDialog();
    }
}
