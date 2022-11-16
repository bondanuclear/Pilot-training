using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup")] 
     [SerializeField] private float xSpeed = 15f;
    [SerializeField] private float ySpeed = 15f;
    [SerializeField] float xRange = 8f;
    float horizontalInput, verticalInput;
    [SerializeField] float pitchFactor = -2f;
    [SerializeField] float pitchInputFactor = -5f;
    [SerializeField] float rollInputFactor = -15f;
    [SerializeField] float yawFactor = 1.54f;
    [SerializeField] GameObject[] lasers;
    public int hitPower = 1;
    public bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive) {
            ProcessInput();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessFiring()
    {
        if(Input.GetButton("Fire1")) {
            SetLasersActive(true);
        }
        else {
            SetLasersActive(false);
        }   
    }

    private void SetLasersActive(bool isActive)
    {
        foreach(GameObject laser in lasers) {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

    

    void ProcessRotation() 
    {
        float pitch = transform.localPosition.y * pitchFactor + pitchInputFactor * verticalInput;
        // pitch rotation depends on both input and position
        float yaw = transform.localPosition.x * yawFactor;
        // 
        float roll = rollInputFactor * horizontalInput;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    void ProcessInput()
    {
         horizontalInput = Input.GetAxis("Horizontal");
         verticalInput = Input.GetAxis("Vertical");

        float offsetX = horizontalInput * Time.deltaTime * xSpeed;
        float offsetY = verticalInput * Time.deltaTime * ySpeed;

        float rawXPos = transform.localPosition.x + offsetX;
        float rawYPos = transform.localPosition.y + offsetY;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -5f, 8f);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
