using UnityEngine;
using UnityEngine.UI;

public class Scroll2 : MonoBehaviour
{
    public Text m_Explanations;
    private string[] m_Text = { "Des indices",
                                "Encore des indices",
                                "Toujours des indices" };
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
