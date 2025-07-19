using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class JobApplication : MonoBehaviour
{
    public int identifier;

    //ontriggerenter set in GameManager as currentApp
    public string applicationText;

    public Text applicationTextUI;


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
    }
    
}
