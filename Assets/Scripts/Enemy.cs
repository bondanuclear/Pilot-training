using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    GameObject parent;
    [SerializeField] int healthBar;

    ScoreBoard scoreBoard;
    [SerializeField] int points = 3;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {       
        parent = GameObject.FindWithTag("SpawnAtRuntime");
        gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().useGravity = false;
        playerController = FindObjectOfType<PlayerController>();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        if(healthBar <= 0) 
        {
            ProcessDeath();
        }
        else
        {
            ProcessDamage();
        }

    }

    private void ProcessDamage()
    { 
        healthBar -= playerController.hitPower;
        Debug.Log(healthBar);
    }

    private void ProcessDeath()
    {
        scoreBoard.IncreaseScore(points);
        GameObject vfx = Instantiate(explosion, transform.position, Quaternion.identity);
        vfx.transform.parent = parent.transform;
        Destroy(gameObject);
    }
}
