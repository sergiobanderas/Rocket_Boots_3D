using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float rotationForce = 100f;
    Rigidbody rb;

    void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rb.AddRelativeForce(Vector3.up * moveForce);
        }
    }

    private void ProcessRotation()
    {
        float rotationValue = rotation.ReadValue<float>();
        if (rotationValue < 0)
        {            
            transform.Rotate(Vector3.forward * rotationForce * Time.fixedDeltaTime);
        }else if (rotationValue > 0)
        {
            transform.Rotate(-Vector3.forward * rotationForce * Time.fixedDeltaTime);
        }
    }
}
