using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadZone : MonoBehaviour
{
    public int iLevelToLoad;
    public string sLevelToLoad;

    public bool useIntegerToLoadLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.name == "Player")
        {
            LoadScene();
        }

        void LoadScene()
        {
            if (useIntegerToLoadLevel)
            {
                SceneManager.LoadScene(iLevelToLoad);
            }

            else
            {
                SceneManager.LoadScene(sLevelToLoad);
            }
        }

    }
}
