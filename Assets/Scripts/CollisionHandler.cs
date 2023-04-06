using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 1f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem winParticle;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool isCollisionDisabled = false;
     void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
     void Update()
    {
        RespondtoDebugKeys();
    }

     void RespondtoDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            isCollisionDisabled = !isCollisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || isCollisionDisabled)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "BasePoint":
                Debug.Log("This thing is your basepoint!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("You got fuel!");
                break;
            default:
                StartCrashSequence();

                break;
        }
    }

    void StartSuccessSequence()
    {
        winParticle.Play();
        isTransitioning = true; 
        audioSource.Stop();
        audioSource.PlayOneShot(winSound);
        GetComponent<Movements>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void StartCrashSequence()
    {
        crashParticle.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        GetComponent<Movements>().enabled = false;
        Invoke("ReloadScene", levelLoadDelay);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);

    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
