using UnityEditor.Animations;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    public AnimatorController animController;

    public AnimatorOverrideController overrideController;
    public string originalClip; // The clip to be replaced
    public AnimationClip newClip;      // The clip to replace with

    void Start()
    {
        // Get the Animator component
        Animator animator = GetComponent<Animator>();

        // Create a new AnimatorOverrideController instance if not already assigned
        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }

        // Set the new clip for the original clip
        overrideController[originalClip] = newClip;

        // Apply the override controller to the Animator
        animator.runtimeAnimatorController = overrideController;
    }

    public void PlayeClip(string clip)
    {
        
    }
}
