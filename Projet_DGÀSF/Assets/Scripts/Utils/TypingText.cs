using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class TypingText
{
    public static IEnumerator Type(TextMeshProUGUI textDisplay, string sentence, float typingSpeed, GameObject nextButton)
    {
        return Type(textDisplay, sentence, typingSpeed, nextButton, null);
    }

    public static IEnumerator Type(TextMeshProUGUI textDisplay, string sentence, float typingSpeed, Action method)
    {
        return Type(textDisplay, sentence, typingSpeed, null, method);
    }

    public static IEnumerator Type(TextMeshProUGUI textDisplay, string sentence, float typingSpeed, GameObject nextButton, Action method)
    {
        textDisplay.text = "";
        bool insideBalise = false;
        foreach (char letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;

            // Ignorer les balises rich text
            if (letter == '<') insideBalise = true;
            // Debug.Log(insideBalise);
            if (!insideBalise)
            {
                yield return new WaitForSeconds(typingSpeed);
            }
            if (letter == '>') insideBalise = false;
        }
        yield return new WaitForSeconds(typingSpeed * 2);

        if (nextButton != null) nextButton.SetActive(true);
        if (method != null) method();
    }
}