using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Parameters
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float rotationForce = 100f;

    [SerializeField] private AudioClip thrustAudioClip;
    
    [SerializeField] private ParticleSystem thrustParticleSystem;
    [SerializeField] private ParticleSystem leftThrusterParticleSystem;
    [SerializeField] private ParticleSystem rightThrusterParticleSystem;    

    //Cached component references
    Rigidbody rb;
    AudioSource audioSource;

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        ProcessThrust();

        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        thrustParticleSystem.Stop();
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * moveForce);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustAudioClip);
        }

        if (!thrustParticleSystem.isPlaying)
        {
            thrustParticleSystem.Play();
        }
    }

    private void ProcessRotation()
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        float rotationValue = rotation.ReadValue<float>();
        if (rotationValue < 0)
        {
            RotateRight();
        }
        else if (rotationValue > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotation();
        }
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }

    private void StopRotation()
    {
        leftThrusterParticleSystem.Stop();
        rightThrusterParticleSystem.Stop();
    }

    private void RotateLeft()
    {
        transform.Rotate(-Vector3.forward * rotationForce);
        if (!leftThrusterParticleSystem.isPlaying)
        {

            leftThrusterParticleSystem.Play();
        }
    }

    private void RotateRight()
    {
        transform.Rotate(Vector3.forward * rotationForce);
        if (!rightThrusterParticleSystem.isPlaying)
        {
            leftThrusterParticleSystem.Stop();
            rightThrusterParticleSystem.Play();
        }
    }
}
