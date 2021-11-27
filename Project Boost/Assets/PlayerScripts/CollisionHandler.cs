using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] AudioClip CollisionClip;
    [SerializeField] AudioClip successClip;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
         switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Is Friendly");
                break;
            case "Finish":
                StartCrashSequence();
                audioSource.PlayOneShot(successClip);
                Invoke("LoadScene", 2);
                break;
            default:
                audioSource.PlayOneShot(CollisionClip);
                StartCrashSequence();
                Invoke("restart", 2);
                break;
        }
    }

    void restart()
    {
        SceneManager.LoadScene("BaseLevel1");
    }

    void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
    }

    private void LoadScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene("BaseLevel2");
        }
        else if (SceneManager.GetActiveScene().buildIndex >= SceneManager.sceneCount)
        {
            SceneManager.LoadScene("BaseLevel1");
        }
    }

}
