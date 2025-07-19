using DG.Tweening;
using UnityEngine;

public class ApplicationSpawner : MonoBehaviour
{
    
    public GameObject[] applicationPrefab; // Prefab for the application object

    public Transform endPos;

    public float timer = 15f;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            SpawnApplication();
        }
    }

    private void Start()
    {
        
    }
    public void SpawnApplication()
    {
        int randApp = Random.Range(0, applicationPrefab.Length);
        var app = Instantiate(applicationPrefab[randApp], transform.position, Quaternion.identity, transform).GetComponent<JobApplication>();
        app.StartConveyor();

        GameManager.Instance.GetApplicationText(app);

        app.transform.DOMove(endPos.position, timer).OnComplete(()=> 
        { 
            Debug.Log("Reach end of Conveyorbelt");
            GameManager.Instance.DenyApplication(app.GetComponent<JobApplication>());
        });    
    }
}
