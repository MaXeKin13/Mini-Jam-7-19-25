using UnityEngine;

public class FlavorText : MonoBehaviour
{
    public string[] flavorText;
    private string x;
    private string y;
    private string z;

    public static FlavorText Instance;

    private void OnEnable()
    {
        Instance = (Instance == null) ? this : Instance;
        //SetFlavorText();
        
    }
    void SetFlavorText()
    {
        flavorText = new string[]
        {
               "Hello, \r\nMy name is Random Guy, and I am very excited for the opportunity to be hired by you.\r\nI like my {0}  and my dog who is also my daughter. \r\nFor the six years of the development of my mental abilities, which I have gained at the age of 16 when my father gambled away my college funding I have, very fortunately, been left with no choice but to apply into your bloodsucking company. \r\nI am very proficient with {1}  and like candy corn in my mouth.\r\nUhmmmm Yummy Yummy Candy corn in my mouth. It feels so good on my tongue. The best feeling since the death of my sister whom i was running a restaurant with. \r\nMy Profficiencies Include {2}  buying beer at the local supermarket \r\nIn any way i really hope you can hire me, i really like paying my rent and having private health insurance.\r\n",

        };
    }

    public string GetRandomFlavorText()
    {
        int randIndex = Random.Range(0, flavorText.Length);
        return flavorText[randIndex];
    }

    /*public string GetFlavorText(int in1, int in2, int in3)
    {
       
    }*/
}
