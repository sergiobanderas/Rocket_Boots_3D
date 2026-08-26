using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionManager : MonoBehaviour
{

    [SerializeField] private float reloadDelay = 1f;
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private AudioClip landingAudioClip;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "LaunchPad":
                Debug.Log("Collided with landing pad");
                break;
            case "LandingPad":
                LandingPadCollision();
                break;
            case "Explosion":
                ExplosionCollision();
                break;
            default:
                break;
        }
        
    }

    private void LandingPadCollision()
    {
        audioSource.PlayOneShot(landingAudioClip);
        GetComponent<PlayerMovement>().enabled = false;
        Invoke(nameof(NextScene), reloadDelay);
    }

    private void ExplosionCollision()
    {
        audioSource.PlayOneShot(explosionAudioClip);
        GetComponent<PlayerMovement>().enabled = false;
        Invoke(nameof(ReloadScene), reloadDelay);
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
