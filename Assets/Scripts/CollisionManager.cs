using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{

    [SerializeField] private float reloadDelay = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "LaunchPad":
                Debug.Log("Collided with landing pad");
                break;
            case "LandingPad":
                GetComponent<PlayerMovement>().enabled = false;
                Invoke(nameof(NextScene), reloadDelay);
                break;
            case "Explosion":
                GetComponent<PlayerMovement>().enabled = false;
                Invoke(nameof(ReloadScene), reloadDelay);
                break;
            default:
                break;
        }
        
    }

    private void ReloadScene()
    {        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NextScene()
    {        
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
}
