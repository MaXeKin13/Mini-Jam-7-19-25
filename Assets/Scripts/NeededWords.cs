using TMPro;
using UnityEngine;

public class NeededWords : MonoBehaviour
{
    public TextMeshProUGUI neededWordsText;

    private void Start()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        
            string[] neededWords = GameManager.Instance.GetNeededApplicationsText();
        Debug.Log(neededWords);
        neededWordsText.text = "Needed Words:\n" + neededWords[0] + " " + neededWords[1] + " " + neededWords[2];
    }
}
