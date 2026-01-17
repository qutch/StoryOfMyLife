using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    Rigidbody2D rigidbodyElement;
    Vector2 movement;

    [Header("Visuals")]
    public SpriteRenderer spriteRenderer;
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;

    public int maxWords = 10;
    private string[] learnedVocabulary;
    private int numWordsLearned = 0;
    public string[] vocabList { get { return learnedVocabulary; } }
    public int numWords { get { return numWordsLearned; } }

    void Start()
    {
        MoveAction.Enable();
        rigidbodyElement = GetComponent<Rigidbody2D>();
        learnedVocabulary = new string[maxWords];

        // Automatic fallback if you forget to assign the SpriteRenderer
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        movement = MoveAction.ReadValue<Vector2>();

        // Update the sprite based on movement direction
        UpdateSprite(movement);
    }

    void UpdateSprite(Vector2 moveInput)
    {
        if (moveInput.magnitude > 0.1f)
        {
            // Vertical Movement
            if (Mathf.Abs(moveInput.y) > Mathf.Abs(moveInput.x))
            {
                spriteRenderer.flipX = false; // Reset flip when moving up/down
                if (moveInput.y > 0) spriteRenderer.sprite = spriteUp;
                else spriteRenderer.sprite = spriteDown;
            }
            // Horizontal Movement
            else 
            {
                if (moveInput.x > 0) 
                {
                    spriteRenderer.sprite = spriteRight;
                    spriteRenderer.flipX = false; // Face Right
                }
                else 
                {
                    spriteRenderer.sprite = spriteRight; // Use Right sprite...
                    spriteRenderer.flipX = true;  // ...but flip it!
                }
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 newPosition = (Vector2)rigidbodyElement.position + movement * 3.0f * Time.deltaTime;
        rigidbodyElement.MovePosition(newPosition);
    }

    public void addVocabWord(string word)
    {
        if (numWordsLearned < maxWords)
        {
            // FIXED: Set the word first, then increment the counter
            learnedVocabulary[numWordsLearned] = word;
            Debug.Log("Learned: " + learnedVocabulary[numWordsLearned]);
            numWordsLearned += 1;
        }
    }
}