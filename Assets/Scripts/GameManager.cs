using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    


    [Header("Needed Applications")]
    public List<NeededApplication> neededApplications = new List<NeededApplication>();



    public JobApplication currentApp;

    public Dictionary<int, string> keyValuePairs = new Dictionary<int, string>();

    public string[] applicationWords;

    public static GameManager Instance;

    [Header("Health System")]
    public int fullHealth;
    public int currentHealth;
    public TextMeshProUGUI healthText;

    public NeededWords neededWordsText;
    public ApplicationSpawner spawner;

    public AudioManager audioManager;
    private void Awake()
    {
        Instance = (Instance == null) ? this : Instance;
        SetDictionary();

        UpdateHealthText();

        DOVirtual.DelayedCall(1f, () =>
        {
            spawner.SpawnApplication();
        });
        
    }

    private void UpdateHealthText()
    {
        healthText.text = "Health: " + currentHealth + "/" + fullHealth;
    }

    private void SetDictionary()
    {
        for (int i = 0; i < applicationWords.Length; i++)
        {
            keyValuePairs.Add(i, applicationWords[i]);
        }
    }
    public void GetApplicationText(JobApplication app)
    {
        //set text from dictionary  
        if (keyValuePairs.TryGetValue(app.identifier, out string text))
        {
            int randomText = UnityEngine.Random.Range(0, app.replacementTexts.Length);
            app.replacementTexts[randomText] = text;

            // Set other replacementTexts as random texts from applicationWords[]  
            for (int i = 0; i < app.replacementTexts.Length; i++)
            {
                if (i != randomText)
                {
                    int randomIndex;
                    do
                    {
                        randomIndex = UnityEngine.Random.Range(0, applicationWords.Length);
                    }
                    while (neededApplications.Exists(neededApp => neededApp.identifier == randomIndex));

                    app.replacementTexts[i] = applicationWords[randomIndex];
                }
            }

            Debug.Log("Get application Text " + app.identifier);
        }
        else
        {
            //set all random  
            for (int i = 0; i < app.replacementTexts.Length; i++)
            {
                int randomIndex;
                do
                {
                    randomIndex = UnityEngine.Random.Range(0, keyValuePairs.Count);
                }
                while (neededApplications.Exists(neededApp => neededApp.identifier == randomIndex));

                app.replacementTexts[i] = keyValuePairs[randomIndex];
            }
            app.SetText();

            Debug.Log("Identifier not found in dictionary: " + app.identifier);
        }
        app.SetText();
    }
    public void CheckApplication(JobApplication app)
    {
        
        //check if app = needed application
        bool isMatchFound = false;
        foreach (NeededApplication neededApp in neededApplications)
        {
            Debug.Log("Application " + app.identifier);
            if (app.identifier == neededApp.identifier)
            {
                isMatchFound = true;
                if (neededApp.count <= 0)
                {
                    //if count is 0, return  
                    Debug.Log("Application " + app.identifier + " quota already met.");
                    FailApplication(-2);
                    break;
                }
                //increment count  
                Debug.Log("Application " + app.identifier + " accepted.");
                neededApp.count--;
                neededWordsText.UpdateText(); // Update the needed words text display
                StampApplication();
                break;
            }
        }

        if (!isMatchFound)
        {
            Debug.Log("Application " + app.identifier + " is not needed.");
            FailApplication(-2);
        }

        spawner.SpawnApplication(); // Spawn a new application after checking the current one

    }

    public void DenyApplication(JobApplication app)
    {
        bool isMatchFound = false;
        foreach (NeededApplication neededApp in neededApplications)
        {
            if (app.identifier == neededApp.identifier)
            {
                isMatchFound = true;
                if (neededApp.count <= 0)
                {
                    //if count is 0, return  
                    Debug.Log("SUCCEED: Application " + app.identifier + " not needed");
                    FailApplication(0);
                    break;
                }
                //increment count  
                Debug.Log("FAIL: Application " + app.identifier + " needed.");
                FailApplication(-2);
                //Destroy(currentApp.gameObject);
                break;
            }
        }

        if (!isMatchFound)
        {
            Debug.Log("Application " + app.identifier + " is not needed.");
            FailApplication(0);
        }

        spawner.SpawnApplication();
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CheckApplication(currentApp);
        }
        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            DenyApplication(currentApp);
        }
    }

    public void SetApplication(JobApplication app)
    {
        Debug.Log("SettingApplication");
        currentApp = app;
    }
    public void ResetApp()
    {
        currentApp = null;
    }
    private void StampApplication()
    {
        //animation + stamp
        UpdateHealth(1);

        currentApp.SuccessAnimation();

        currentApp = null;
        //Destroy(currentApp.gameObject);

    }
    public void FailApplication(int num)
    {
        UpdateHealth(num);
        audioManager.PlayRandomScream();
        currentApp.FailAnimation();
        //Destroy(currentApp.gameObject);
        currentApp = null;
    }




    public void UpdateHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > fullHealth)
        {
            currentHealth = fullHealth;
        }
        else if (currentHealth < 0)
        {
            FailGame();
        }

        Debug.Log("Current Health: " + currentHealth);

        UpdateHealthText();
    }

    private void FailGame()
    {
        Application.Quit();
    }


    public string[] GetNeededApplicationsText()
    {
        string[] neededTexts = new string[neededApplications.Count];
        for (int i = 0; i < neededApplications.Count; i++)
        {
            NeededApplication neededApp = neededApplications[i];
            if (keyValuePairs.TryGetValue(neededApp.identifier, out string text))
            {
                neededTexts[i] = text;
            }
            else
            {
                neededTexts[i] = $"Unknown Application {neededApp.identifier} x{neededApp.count}";
            }
        }
        return neededTexts;
    }
    [Serializable]
    public class NeededApplication
    {
        public int identifier;
        public int count;     
    }
}
