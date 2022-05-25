using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadZone : MonoBehaviour
{
    public int iLevelToLoad;
    public string sLevelToLoad;

    public bool useIntegerToLoadLevel = false;

    private void OnTriggerEnter(Collider collision)
    {
        //Loads new scene if player enters collider
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.name == "Player")
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
