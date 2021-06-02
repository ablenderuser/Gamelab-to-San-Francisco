using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
// required for display in unity Inspector tab (as it is not a MonoBehavior)

public class Dialog
{
    [TextArea(1, 5)] // better input field in Unity Inspector

    public int m_PNJIndex;
    public string m_PNJMood;
    public string m_Sentence;
    public string m_PlayerMood;
    public string[] m_Choices;
    public Dialog[] m_NextSentences;

    public static Dialog CreateFromJSON(string jsonString)
    {
        return JsonConvert.DeserializeObject<Dialog>(jsonString);
    }
}
