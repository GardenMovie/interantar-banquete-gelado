using UnityEngine;

public class animatorChooser : MonoBehaviour
{
    Animator PlayerAnimator;

    // inspector-assigned list of animator controllers
    public RuntimeAnimatorController[] animatorControllers;

    int index = GameManager.Instance.characterChoice;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerAnimator = GetComponent<Animator>();

        // expects GameManager.selectedCharacterAnimator to be an int index

        if (animatorControllers != null && index >= 0 && index < animatorControllers.Length)
        {
            PlayerAnimator.runtimeAnimatorController = animatorControllers[index];
        }
        else
        {
            Debug.LogWarning($"animatorChooser: selectedCharacterAnimator index {index} out of range. Make sure animatorControllers has that index set in the inspector.");
        }
    }
}
