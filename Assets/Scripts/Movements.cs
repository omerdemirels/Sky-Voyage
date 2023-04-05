using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    [SerializeField] private float mainThrust =1000f;
    [SerializeField] private float rotationForce = 100f;
    [SerializeField] AudioClip baloonSound;
    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rb.AddRelativeForce(Vector3.up * mainThrust* Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(baloonSound);
            }
        }
        else
        {
            audioSource.Stop();
        }
        
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationForce);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationForce);
        }
    }

    public void ApplyRotation(float rotationSide)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationSide * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
