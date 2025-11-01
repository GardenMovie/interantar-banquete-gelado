using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Vector2 moveInput = new Vector2(0,0);
    public float moveSpeed = 5f;
    public bool Strong = false;
    public int offangle = -180;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Animator animatorController;
    public Tilemap groundTilemap;
    public Tilemap waterTilemap;

    private int state = 0;

    void Awake()
    {
        animatorController = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Vector3Int cellPosition = groundTilemap.WorldToCell(transform.position);

        state = groundTilemap.HasTile(cellPosition) ? 0 : 1 ;

        // Check if there's a tile in water
       if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            // ChangePlayerChar(1);
            state = 0;
        }
        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            // ChangePlayerChar(1);
            state = 1;
        }
            if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            // ChangePlayerChar(1);
            GameManager.Instance.AddHealth(-1);
        }   // else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        // {
        //     ChangePlayerChar(2);
        // }
        // else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        // {
        //     ChangePlayerChar(3);
        // }
        // else if (Keyboard.current.digit4Key.wasPressedThisFrame)
        // {
        //     ChangePlayerChar(4);}
        if (moveInput != new Vector2(0, 0))
        {
            if (state == 1)
            {
                animatorController.Play("AdeliaNadoAnim");
                spriteRenderer.flipY = true;
                spriteRenderer.sortingOrder = -2;
                return;
            }
            else
            {
                spriteRenderer.sortingOrder = 3;
                animatorController.Play("AdeliaWalkAnim");
                spriteRenderer.flipY = false;
            }
        }
        else
        {
            if (state == 1)
            {
                animatorController.StopPlayback();
                return;
            }
            animatorController.Play("AdeliaStand");
        }
        } 


    void ChangePlayerChar(int Char)
    {
        switch (Char)
        {
            case 1:
                spriteRenderer.color = Color.red;
                Strong = true;
                // boxCollider2D.size = new Vector2 (1.5f,1.5f);
                gameObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                break;

            case 2:
                spriteRenderer.color = Color.blue;
                Strong = false;
                // boxCollider2D.size = new Vector2 (1,1);
                gameObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
                break;
            case 4:
                GameManager.Instance.ChangeScene("FinalMenu");
                break;

            default:
                spriteRenderer.color = Color.green;
                Strong = false;
                // boxCollider2D.size = new Vector2 (1,1);
                gameObject.transform.localScale = new Vector3(0.25f,0.25f,0.25f);
                break;
        }
    }


    // Called by Input System
    public void SetMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;

        if (moveInput != new Vector2(0, 0))
        {
            transform.rotation = Quaternion.Euler(0, 0, angle-offangle);
        }
        Vector2 movement = moveInput * moveSpeed;
        Vector2 newPosition = rb.position + movement * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
