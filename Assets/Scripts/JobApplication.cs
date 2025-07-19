using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class JobApplication : MonoBehaviour
{
    public FlavorText flavorText;
    public int identifier;

    //ontriggerenter set in GameManager as currentApp
    public string applicationText;
    public string[] replacementTexts;

    public TextMeshProUGUI applicationTextUI;


    public float appTimer = 5f;


    private void Start()
    {
        GameManager.Instance.currentApp = this;

        //replacementTexts = new string[3];
    }

    public void StartConveyor()
    {
        //transform.DOMove()
    }
   
    public void SetText()
    {
        //applicationText = flavorText.Get

        Debug.Log(replacementTexts[0] + replacementTexts[1] + replacementTexts[2]);

        applicationText = FlavorText.Instance.GetRandomFlavorText();

        string formattedText = string.Format(applicationText, replacementTexts);

        applicationTextUI.text = formattedText;
    }

    /*public void SetText()
    {
        if (GameManager.Instance.keyValuePairs.TryGetValue(identifier, out string text))
        {
            applicationText = text;
            applicationTextUI.text = applicationText;
        }
        else
        {
            Debug.LogError("Identifier not found in dictionary: " + identifier);
        }
        //choose random spot
        int randomText = Random.Range(0, 2);
        string flavorText = "this this this " + GameManager.Instance.keyValuePairs.TryGetValue(identifier, out string t);
    }*/
    
}
