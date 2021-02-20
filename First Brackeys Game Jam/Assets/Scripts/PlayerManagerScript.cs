﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManagerScript : MonoBehaviour
{
    public Transform cameraPosition;
    public Camera cameraOrthographic;
    public Vector3 offsetCameraPosition,
                   offsetLocalScale;

    private RandomSpawnScript randomSpawnScript;

    public int playerStrength;

    // Start is called before the first frame update
    void Start()
    {
        playerStrength = 0;

        randomSpawnScript = GetComponent<RandomSpawnScript>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition.position = transform.position + offsetCameraPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Enemy"))
        {
            if (collision.GetComponent<EnemyAIScript>().enemySizeID == 0 && playerStrength == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                if (collision.GetComponent<EnemyAIScript>().enemySizeID > playerStrength)
                {
                    Destroy(gameObject);
                }
                else if (collision.GetComponent<EnemyAIScript>().enemySizeID == playerStrength)
                {
                    transform.localScale = transform.localScale - offsetLocalScale;
                    playerStrength--;
                    cameraOrthographic.orthographicSize--;
                    Destroy(collision.gameObject);
                }
                else if (collision.GetComponent<EnemyAIScript>().enemySizeID < playerStrength)
                {
                    Destroy(collision.gameObject);
                }
            }
        }

        if (collision.tag.Contains("Neutral"))
        {
            if (collision.GetComponent<SlimeAIScript>().neutralSizeID == 0 && playerStrength == 0)
            {
                transform.localScale = transform.localScale + offsetLocalScale;
                playerStrength++;
                cameraOrthographic.orthographicSize++;

                Destroy(collision.gameObject);
            }
            else
            {
                if (collision.GetComponent<SlimeAIScript>().neutralSizeID == playerStrength)
                {
                    transform.localScale = transform.localScale + offsetLocalScale;
                    playerStrength++;
                    cameraOrthographic.orthographicSize++;

                    Destroy(collision.gameObject);
                }
            }
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Contains("Enemy"))
        {
            if (collision.collider.GetComponent<EnemyAIScript>().enemySizeID == 0 && playerStrength == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                if (collision.collider.GetComponent<EnemyAIScript>().enemySizeID > playerStrength)
                {
                    Destroy(gameObject);
                }
                else if (collision.collider.GetComponent<EnemyAIScript>().enemySizeID == playerStrength)
                {
                    transform.localScale = transform.localScale - offsetLocalScale;
                    playerStrength--;
                    Destroy(collision.collider.gameObject);
                }
                else if (collision.collider.GetComponent<EnemyAIScript>().enemySizeID < playerStrength)
                {
                    Destroy(collision.collider.gameObject);
                }
            }
        }

        if (collision.collider.tag.Contains("Neutral"))
        {
            if (collision.collider.GetComponent<SlimeAIScript>().neutralSizeID == 0 && playerStrength == 0)
            {
                transform.localScale = transform.localScale + offsetLocalScale;
                playerStrength++;
                Destroy(collision.collider.gameObject);
            }
            else
            {
                if (collision.collider.GetComponent<SlimeAIScript>().neutralSizeID == playerStrength)
                {
                    transform.localScale = transform.localScale + offsetLocalScale;
                    playerStrength++;
                    Destroy(collision.collider.gameObject);
                }
            }
        }
    }
    */
}