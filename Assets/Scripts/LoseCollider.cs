using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        if (FindObjectsOfType<Ball>().Length == 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
