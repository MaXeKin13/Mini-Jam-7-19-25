using UnityEngine;

public class FlavorText : MonoBehaviour
{
    public string[] flavorText;
    private int x;
    void SetFlavorText()
    {
        flavorText = new string[]
        {
               "The wind whispers through the trees." + x + "",
               "A faint glow illuminates the horizon.",
               "The air is thick with anticipation.",
               "Shadows dance across the walls.",
               "A distant howl pierces the silence."
        };
    }
}
