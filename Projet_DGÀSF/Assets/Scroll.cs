using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroll : MonoBehaviour
{
    public Text m_Explanations;
    private string[] m_Text = { "Bienvenue dans la galaxie des cuillères, dont VOUS êtes le héros !" +
                                "\nUn monde où les choses fonctionnent quelque peu différemment… ",
                                "Ici, chacune de vos actions aura des conséquences, même la plus anodine…" +
                                "\nCe qui est considéré comme un handicap dans votre monde, ici, est devenu la norme." +
                                "\nPar conséquent, l’énergie, appelée cuillère, n’a pas la même valeur et s’épuise beaucoup plus vite vous forçant à faire des choix drastiques. ",
                                "Pas de mages, de guerriers ou de soigneurs mais des autistes, des bipolaires et des fibromyalgiques…" +
                                "\nPas de cœurs, d’énergie ou de mana, ici ce sont les cuillères qui font la loi." +
                                "\nUne loi qui limite vos cuillères à un nombre donné dans la journée." +
                                "\nVous allez en perdre ou en gagner, mais de quelle manière ?" +
                                "\n\nCela ne dépend que de vous… " };
    private int m_Index = 0;
    public ChangeurDeScene m_SceneChanger;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Explanations.text = m_Text[m_Index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void continueExplanations()
    {
        if (m_Index < 2)
        {
            m_Index += 1;
            m_Explanations.text = m_Text[m_Index];
        }
        else
        {
            m_SceneChanger.changeScene();
        }
    }
}
