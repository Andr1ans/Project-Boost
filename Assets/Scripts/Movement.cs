using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readability for speed
    // STATE = private instance (member) variables  
    [SerializeField]float mainThrust = 100f;
    [SerializeField]float rotationThrust = 1f;
    [SerializeField]AudioClip mainEngine;
    [SerializeField]ParticleSystem mainEngineParticles;
    [SerializeField]ParticleSystem rightThrusterParticles;
    [SerializeField]ParticleSystem leftThrusterParticles;
    Rigidbody myRigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
         else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        myRigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); //Vector3.up = 0, 1, 0 (x, y, z)
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainEngineParticles.isPlaying) 
            {
                mainEngineParticles.Play();
            }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(-rotationThrust);
          if (!rightThrusterParticles.isPlaying) 
            {
                rightThrusterParticles.Play();
            }
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
            if (!leftThrusterParticles.isPlaying) 
            {
                leftThrusterParticles.Play();
            }
    }
    
    void StopRotating()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
         myRigidBody.freezeRotation = true; // Freezing rotation so we can manually rotate
         transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
         myRigidBody.freezeRotation = false; // Unfreezing rotation so the physics system can take over
    }
}
