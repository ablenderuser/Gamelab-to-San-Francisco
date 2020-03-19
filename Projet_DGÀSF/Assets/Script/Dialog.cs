using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// required for display in unity Inspector tab (as it is not a MonoBehavior)

public class Dialog
{
    [TextArea(1, 5)] // better input field in Unity Inspector
    public string m_Sentence;
    public string[] m_Choices;
    public Dialog[] m_NextSentences;
}
