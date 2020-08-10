using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    //cached references
    SceneLoader sceneLoader;

    [SerializeField] public int totalBlocks = 0; //Serialized for debuging purposes

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        totalBlocks++;
    }

	public void BlockDestroyed()
    {
        totalBlocks--;
        if(totalBlocks <= 0) {
            Time.timeScale = 0.3f;
            sceneLoader.LoadNextScene();       
        }
    }

    
}
