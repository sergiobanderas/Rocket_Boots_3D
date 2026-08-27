using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CollisionManager : MonoBehaviour
{

    [SerializeField] private float reloadDelay = 1f;
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private AudioClip landingAudioClip;
    [SerializeField] private ParticleSystem explosionParticleSystem;
    [SerializeField] private ParticleSystem landingParticleSystem;

    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isControllable = true;
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            NextScene();
        }else if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ReloadScene();
        }else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isControllable || !isCollidable) { return; }

        switch (collision.gameObject.tag)
        {
            case "LaunchPad":
                Debug.Log("LaunchPad");
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
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(landingAudioClip);
        landingParticleSystem.Play();
        GetComponent<PlayerMovement>().enabled = false;
        Invoke(nameof(NextScene), reloadDelay);
    }

    private void ExplosionCollision()
    {           
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(explosionAudioClip);        
        explosionParticleSystem.Play();
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
