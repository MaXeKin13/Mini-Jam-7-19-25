using System;
using System.Collections.Generic;
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

    private void Awake()
    {
        Instance = (Instance == null) ? this : Instance;

        SetDictionary();
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
                StampApplication();
                break;
            }
        }

        if (!isMatchFound)
        {
            Debug.Log("Application " + app.identifier + " is not needed.");
            FailApplication(-2);
        }
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
                    
                    break;
                }
                //increment count  
                Debug.Log("FAIL: Application " + app.identifier + " needed.");
                FailApplication(-2);
                break;
            }
        }

        if (!isMatchFound)
        {
            Debug.Log("Application " + app.identifier + " is not needed.");
            Destroy(app.gameObject);
        }
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CheckApplication(currentApp);
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

        Destroy(currentApp.gameObject);

    }
    public void FailApplication(int num)
    {
        UpdateHealth(num);

        Destroy(currentApp.gameObject);
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
    }

    private void FailGame()
    {

    }
    [Serializable]
    public class NeededApplication
    {
        public int identifier;
        public int count;     
    }
}
