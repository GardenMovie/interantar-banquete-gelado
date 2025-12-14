using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public AudioClip walkSound;
    public float stepDelay = 0.5f;
    public float stepVolume = 0.3f;
    private Coroutine repeatedSFX;
    private bool isPlayingSFX = false;

    private Vector2 moveInput = new Vector2(0,0);
    public float moveSpeed = 5f;
    public int FastSwim = 0;
    public int FastSwimCoef = 0;
    public int offangle = -180;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animatorController;
    public Tilemap groundTilemap;
    public Tilemap waterTilemap;
    private CapsuleCollider2D PlayerHitbox;
    private Vector2 OriginalSize;

    private int state = 0;

    void Awake()
    {
        animatorController = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PlayerHitbox = GetComponent<CapsuleCollider2D>();
        OriginalSize = PlayerHitbox.size;
    }

    void Update()
    {
        Vector3Int cellPosition = groundTilemap.WorldToCell(transform.position);

        state = groundTilemap.HasTile(cellPosition) ? 0 : 1 ;

        if (moveInput != new Vector2(0, 0))
        {
            if (state == 1)
            {
                if(isPlayingSFX)
                {
                    StopCoroutine(repeatedSFX);
                    isPlayingSFX = false;
                }
                animatorController.Play("swim", 0);
                FastSwim = 1;
                spriteRenderer.flipY = true;
                spriteRenderer.sortingOrder = -1;
                // CapsuleCollider2D.size = Vector2(CapsuleCollider2D.size.x, CapsuleCollider2D.size.y * 2);
                PlayerHitbox = PlayerHitbox ?? GetComponent<CapsuleCollider2D>();
                if (PlayerHitbox != null)
                    PlayerHitbox.size = new Vector2(OriginalSize.x, OriginalSize.y * 2);
                return;
            }
            else
            {
                FastSwim = 0;
                spriteRenderer.sortingOrder = 3;
                animatorController.Play("walk", 0);
                if (!isPlayingSFX)
                {
                    repeatedSFX = StartCoroutine(PlaySFXRepeated(walkSound,stepDelay));
                    isPlayingSFX = true;
                }
                spriteRenderer.flipY = false;
                PlayerHitbox.size = new Vector2(OriginalSize.x, OriginalSize.y);
                return;
            }
        }
        else
        {
            if(isPlayingSFX)
            {
                StopCoroutine(repeatedSFX);
                isPlayingSFX = false;
            }
            if (state == 1)
            {
                animatorController.StopPlayback();
                return;
            }
            animatorController.Play("idle");
            PlayerHitbox.size = new Vector2(OriginalSize.x, OriginalSize.y);
        }
        } 

    public IEnumerator PlaySFXRepeated(AudioClip myClip, float delay)
    {
        while (true)
        {
            GameManager.Instance.PlaySFX(myClip, stepVolume);
            yield return new WaitForSeconds(delay);
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
        Vector2 movement = moveInput * (moveSpeed + (FastSwim * FastSwimCoef));
        Vector2 newPosition = rb.position + movement * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
