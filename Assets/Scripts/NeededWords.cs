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
        neededWordsText.text = "Needed Words:\n" + neededWords[0] + " " + GameManager.Instance.neededApplications[0].count + " "
            + neededWords[1] + " " + GameManager.Instance.neededApplications[1].count + " " + neededWords[2] + " " + GameManager.Instance.neededApplications[2].count;
    }
}
