using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // name of the scene to load
    [SerializeField] private InputAction interactAction; // key for interaction, set to e
    private bool playerInZone = false;
    void Start()
    {
        interactAction.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            Debug.Log("UI to add: Press E to interact");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }
    private void Update()
    {
        if (playerInZone && interactAction.WasPressedThisFrame())
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}