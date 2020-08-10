using UnityEngine;

public class Ball : MonoBehaviour {
    //config parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPushVelocity = 2f;
    [SerializeField] float yPushVelocity = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    //cached references
    Rigidbody2D ballRigidbody2D;

    //state variables
    Vector2 paddleToBallVector;
    bool hasStarted = false;

	void Start () {
        if(paddle1 == null)
        {
            paddle1 = FindObjectOfType<Paddle>();
        }
        paddleToBallVector = transform.position - paddle1.transform.position;
        ballRigidbody2D = GetComponent<Rigidbody2D>();
    }
	
	void Update ()
    {
        if (!hasStarted && paddle1 != null)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
        ChangeVelocityOnMouseClick();
    }

    public bool getHasStarted()
    {
        return hasStarted;
    }

    private void ChangeVelocityOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ballRigidbody2D.velocity *= 1.1f;
        }
        if (Input.GetMouseButtonDown(1))
        {
            ballRigidbody2D.velocity *= 0.9f;
        }
    }

    private void LaunchOnMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ballRigidbody2D.velocity = new Vector2(xPushVelocity, yPushVelocity);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
        
        if(collision.gameObject != paddle1.gameObject)
        {
            if(ballRigidbody2D.velocity.y <= randomFactor && ballRigidbody2D.velocity.y >= randomFactor * -1)
            {
                ballRigidbody2D.velocity += new Vector2(randomFactor, randomFactor*3);
                Debug.Log("An intervention from software to break ball loop");
            }
        }
    }
}