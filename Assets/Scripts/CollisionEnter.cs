using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionEnter : MonoBehaviour
{
    private Movement movement;
    private AudioSource audioSource;

    [SerializeField] private float levelLoadDelay = 2f;
    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip death;
    [SerializeField] private ParticleSystem successParticle;
    [SerializeField] private ParticleSystem deathParticle;

    private bool isTransioning = false;
    private bool collisionDisable = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();
    }
    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
       else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isTransioning || collisionDisable)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {

            case "Friendly":
                Debug.Log("friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrushSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        isTransioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticle.Play();
        movement.enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartCrushSequence()
    {
        isTransioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticle.Play();
        movement.enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }
    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
