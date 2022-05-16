using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollissionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;
    bool isTransitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Collided with friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                Invoke("LoadNextLevel", levelLoadDelay);
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        // TODO add SFX upon crash
        // TODO add particle effect upon crash  
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(success);
        Invoke("ReloadLeve", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        // TODO add SFX upon crash
        // TODO add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crash);
        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
