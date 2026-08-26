using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "LaunchPad":
                Debug.Log("Collided with landing pad");
                break;
            case "LandingPad":
                Debug.Log("Collided with landing pad");
                break;
            case "Explosion":
                Debug.Log("Collided with explosion");
                break;
            default:
                break;
        }
        
    }
}
