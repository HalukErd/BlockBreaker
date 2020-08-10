using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour {

    //config parameters
    [Range(0.1f, 10f)] [SerializeField] float GameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 83;
    [SerializeField] TextMeshProUGUI scoreTextMeshPro;
    [SerializeField] bool isAutoPlayEnabled;
    
    //state parameters
    [SerializeField] int currentScore = 0; //Serialized for debuging purposes

    private void Awake()
    {
        int countGameStatus = FindObjectsOfType<GameSession>().Length;
        if(countGameStatus > 1)
        {
            gameObject.SetActive(false);
            DestroyObject(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void Start()
    {
        printScoreToTMP();
    }

    void Update()
    {
        Time.timeScale = GameSpeed;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        printScoreToTMP();
    }

    public void DestroyItself()
    {
        Destroy(gameObject);
    }

    private void printScoreToTMP()
    {
        scoreTextMeshPro.text = currentScore.ToString();
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
