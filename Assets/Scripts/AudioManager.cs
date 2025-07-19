using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //public static AudioManager Instance;

    public AudioSource backgroundMusicSource;
    public AudioSource[] Screams;
    private void Awake()
    {
        //Instance = (Instance == null) ? this : Instance;
    }

    public void PlayRandomScream()
    {
        int randomIndex = Random.Range(0, Screams.Length);

        Screams[randomIndex].Play();
    }
}
