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
    public string[] replacementTexts = new string[3];

    public TextMeshProUGUI applicationTextUI;


    public float appTimer = 5f;


    private void Start()
    {
        GameManager.Instance.currentApp = this;
    }

    public void StartConveyor()
    {
        //transform.DOMove()
    }
   
    public void SetText()
    {
        applicationText = GameManager.
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
