using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Transform[] finalSuccesPos;


    public Transform finalPos;

    public GameObject failEffect;


    public static AnimationHandler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
