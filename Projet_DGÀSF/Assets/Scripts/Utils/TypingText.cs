using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class TypingText
{
    // Surcharge pour le bouton suivant
    public static IEnumerator Type(TextMeshProUGUI textDisplay, string sentence, float typingSpeed, GameObject nextButton)
    {
        return Type(textDisplay, sentence, typingSpeed, nextButton, null);
    }

    // Surcharge pour l'appel d'une méthode
    public static IEnumerator Type(TextMeshProUGUI textDisplay, string sentence, float typingSpeed, Action method)
    {
        return Type(textDisplay, sentence, typingSpeed, null, method);
    }

    // Effet d'écriture
    public static IEnumerator Type(TextMeshProUGUI textDisplay, string sentence, float typingSpeed, GameObject nextButton, Action method)
    {
        // Efface le texte initial
        textDisplay.text = "";

        bool insideBalise = false;

        // Boucle sur toutes les lettres 
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

        // Affiche le bouton suivant
        if (nextButton != null) nextButton.SetActive(true);

        // Appele la méthode
        if (method != null) method();
    }
}