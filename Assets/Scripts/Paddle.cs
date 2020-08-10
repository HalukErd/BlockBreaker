using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    //configuration parameters
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minXPos = 1f;
    [SerializeField] float maxXPos = 15f;
    bool isAutoPlayEnabled;

    //cached references
    [SerializeField] GameObject sampleBall;
    Ball theBall;
    GameSession theGameSession;

    private void Start()
    {
        theBall = FindObjectOfType<Ball>();
        theGameSession = FindObjectOfType<GameSession>();
        isAutoPlayEnabled = theGameSession.IsAutoPlayEnabled();
    }

    void Update () {
        Vector2 paddlePos = new Vector2(GetXPos(), transform.position.y);
        paddlePos.x = Mathf.Clamp(paddlePos.x, minXPos, maxXPos);
        transform.position = paddlePos;
        LaunchABallOnMouseClick();
    }

    private void LaunchABallOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject cloneBall = Instantiate(sampleBall, transform.position + new Vector3(1f, 0f, 0f), transform.rotation);
            cloneBall.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 12f);
            Debug.Log("Cheat Alert");
        }
    }

    private float GetXPos()
    {
        if(isAutoPlayEnabled)
        {
            return theBall.transform.position.x;   
        } else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
