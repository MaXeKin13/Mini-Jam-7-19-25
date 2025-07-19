using DG.Tweening;
using UnityEngine;

public class ApplicationSpawner : MonoBehaviour
{
    
    public GameObject[] applicationPrefab; // Prefab for the application object

    public Transform endPos;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            SpawnApplication();
        }
    }
    public void SpawnApplication()
    {
        int randApp = Random.Range(0, applicationPrefab.Length);
        var app = Instantiate(applicationPrefab[randApp], transform.position, Quaternion.identity, transform);
        app.GetComponent<JobApplication>().StartConveyor();

        app.transform.DOMove(endPos.position, 5f).OnComplete(()=> 
        { 
            Debug.Log("Reach end of Conveyorbelt");
            GameManager.Instance.DenyApplication(app.GetComponent<JobApplication>());
        });    
    }
}
