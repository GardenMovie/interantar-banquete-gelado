using UnityEngine;

public class animatorChooser : MonoBehaviour
{
    Animator PlayerAnimator;
    PlayerController PlayerController;
    CircleCollider2D PlayerHitbox;

    // inspector-assigned list of animator controllers
    public RuntimeAnimatorController[] animatorControllers;

    int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        PlayerController = GetComponent<PlayerController>();
        PlayerHitbox = GetComponent<CircleCollider2D>();

        index = GameManager.Instance.characterChoice;
        
        // expects GameManager.selectedCharacterAnimator to be an int index

        if (animatorControllers != null && index >= 0 && index < animatorControllers.Length)
        {
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
        PlayerStats playerStats = GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.UpdateStats(index);
        }
        else
        {
            Debug.LogWarning("animatorChooser: No PlayerStats component found on the GameObject.");
        }
    }
}
