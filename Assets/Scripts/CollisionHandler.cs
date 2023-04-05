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

    AudioSource audioSource;
     void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
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
        //add particle effect
        audioSource.PlayOneShot(winSound);
        GetComponent<Movements>().enabled = false;
        Invoke("NextLevelScene", levelLoadDelay);
    }
    void StartCrashSequence()
    {
        //add particle effect
        audioSource.PlayOneShot(crashSound);
        GetComponent<Movements>().enabled = false;
        Invoke("ReloadScene", levelLoadDelay);
    }
    void NextLevelScene()
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
