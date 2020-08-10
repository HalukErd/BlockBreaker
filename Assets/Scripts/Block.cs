using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    //Configuration params
    [SerializeField] AudioClip destroyClip;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] damagedSprites;
    

    //cached references
    Level level;

    //state variables
    [SerializeField] int timesHit = 0;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            HandleHit();
        }

    }

    private void HandleHit()
    {
        int maxHits = damagedSprites.Length + 1;
        timesHit++;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        } else
        {
            ShowNextSprite();
        }
    }

    private void ShowNextSprite()
    {
        int spriteIndex = timesHit - 1;
        if(damagedSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = damagedSprites[spriteIndex];
        } else
        {
            Debug.LogError("Out of Range in Block Sprite Array! Name: " + gameObject.name);
        }
    }

    private void DestroyBlock()
    {
        FindObjectOfType<GameSession>().AddToScore();
        AudioSource.PlayClipAtPoint(destroyClip, Camera.main.transform.position);
        Destroy(gameObject);
        level.BlockDestroyed(); // to send info for counting
        triggerSparkles();
    }
    
    private void triggerSparkles()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }
}

