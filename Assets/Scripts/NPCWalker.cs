using UnityEngine;
using System.Collections;

public class NPCWalker : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float waitTime = 1f;
    public Vector2[] walkPattern; // Example: (1,0) for right, (0,1) for up

    [Header("Visuals")]
    public SpriteRenderer spriteRenderer;
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;

    private int currentStep = 0;
    private bool isMoving = false;

    void Start()
    {
        // If you forgot to drag the SpriteRenderer in, this finds it automatically
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        // Only start a new move if we aren't already moving and have a pattern
        if (!isMoving && walkPattern.Length > 0)
        {
            StartCoroutine(MoveRoutine());
        }
    }

    IEnumerator MoveRoutine()
    {
        isMoving = true;

        // 1. Determine direction and update the look
        Vector2 moveDirection = walkPattern[currentStep];
        UpdateSpriteDirection(moveDirection);

        // 2. Calculate positions
        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + (Vector3)moveDirection;

        // 3. Smooth movement loop
        float percent = 0;
        while (percent < 1f)
        {
            percent += Time.deltaTime * moveSpeed;
            // This moves the NPC smoothly from start to target
            transform.position = Vector3.Lerp(startPos, targetPos, percent);
            yield return null; 
        }

        // 4. Ensure we land exactly on the tile
        transform.position = targetPos;

        // 5. Setup next step
        currentStep = (currentStep + 1) % walkPattern.Length;
        
        // 6. Wait before next move
        yield return new WaitForSeconds(waitTime);
        
        isMoving = false;
    }

    void UpdateSpriteDirection(Vector2 dir)
    {
        if (spriteRenderer == null) return;

        // We check which direction is the strongest to pick the right sprite
        if (dir.y > 0) spriteRenderer.sprite = spriteUp;
        else if (dir.y < 0) spriteRenderer.sprite = spriteDown;
        else if (dir.x > 0) spriteRenderer.sprite = spriteRight;
        else if (dir.x < 0) spriteRenderer.sprite = spriteLeft;
    }
}