using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CollisionHandler : MonoBehaviour
{
   [SerializeField] ParticleSystem explosionParticle;
  // [SerializeField] AudioClip deathSentence;
   //[SerializeField] GameObject generalBillyImage;
   
   AudioSource audioSource;
    private void Start() {
        //audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other) 
    {
        Debug.Log("Ship has collided with " + other.gameObject.name);   
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log($"{this.name} is triggered by {other.gameObject.name}");
        CrashSequenceLoad();
    }

    private void Respawn()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    void CrashSequenceLoad() 
    {
        GetComponent<PlayerController>().isAlive = false;
        explosionParticle.Play();
        //audioSource.PlayOneShot(deathSentence);
        //generalBillyImage.SetActive(true);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("Respawn", 1f);
    }
}
