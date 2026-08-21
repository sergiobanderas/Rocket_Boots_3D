using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputAction thurst;

    void OnEnable()
    {
        thurst.Enable();
    }

    void Update()
    {
        if (thurst.IsPressed())
        {
            Debug.Log("Thurst is pressed");
        }        
    }

}
