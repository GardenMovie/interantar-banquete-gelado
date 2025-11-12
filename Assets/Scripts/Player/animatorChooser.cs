using UnityEngine;

public class animatorChooser : MonoBehaviour
{
    Animator PlayerAnimator;
    PlayerController PlayerController;
    CapsuleCollider2D PlayerHitbox;

    // inspector-assigned list of animator controllers
    [Header("Animator controllers")]
    public RuntimeAnimatorController[] animatorControllers;

    // moved PlayerStats fields here (set 3 values in inspector)
    [Header("Hitbox sizes per character")]
    public Vector2[] hitboxSizes = new Vector2[3];

    // current hitbox size (kept in sync)
    [HideInInspector]
    public Vector2 hitboxSize = new Vector2(1f, 1f);

    int index;

    void Awake()
    {
        PlayerAnimator = GetComponent<Animator>();
        PlayerController = GetComponent<PlayerController>();
        PlayerHitbox = GetComponent<CapsuleCollider2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        index = GameManager.Instance.characterChoice;

        // expects GameManager.selectedCharacterAnimator to be an int index

        if (animatorControllers != null && index >= 0 && index < animatorControllers.Length)
        {
            if (index == 1) GameManager.Instance.AddHealth(2);
            if (index == 2) PlayerController.moveSpeed = 7;
            PlayerAnimator.runtimeAnimatorController = animatorControllers[index];
            CharStatsUpdate(index);
        }
        else
        {
            Debug.LogWarning($"animatorChooser: selectedCharacterAnimator index {index} out of range. Make sure animatorControllers has that index set in the inspector.");
        }
    }

    void CharStatsUpdate(int index)
    {
        // Prefer direct array values if available
        if (hitboxSizes != null && index >= 0 && index < hitboxSizes.Length)
        {
            Vector2 size = hitboxSizes[index];
            PlayerHitbox = PlayerHitbox ?? GetComponent<CapsuleCollider2D>();
            if (PlayerHitbox != null)
                PlayerHitbox.size = size;

            // keep the local hitboxSize in sync
            hitboxSize = size;
        }
        else
        {
            UnityEngine.Debug.Log("Hitbox sizes does not have an entry for that index");
        }


    }
}