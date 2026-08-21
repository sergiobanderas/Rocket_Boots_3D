using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction thurst;

    Rigidbody rb;

    void OnEnable()
    {
        thurst.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (thurst.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * 10f);
        }        
    }

}
