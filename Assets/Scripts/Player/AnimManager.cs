using UnityEditor.Animations;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    public Animator animController;

    public AnimatorOverrideController overrideController;

    public string originalClip; // The clip to be replaced
    public AnimationClip newClip;      // The clip to replace with

    void Start()
    {
        // Create a new AnimatorOverrideController instance if not already assigned
        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animController.runtimeAnimatorController);
        }

        // Set the new clip for the original clip
        overrideController[originalClip] = newClip;

        // Apply the override controller to the Animator
        animController.runtimeAnimatorController = overrideController;
    }

    public void ReplaceClip(string oldClip, AnimationClip replacement)
    {
        if (overrideController == null)
        {
            overrideController = new AnimatorOverrideController(animController.runtimeAnimatorController);
            animController.runtimeAnimatorController = overrideController;
        }

        if (overrideController[oldClip] != null)
        {
            overrideController[oldClip] = replacement;
        }
        else
        {
            Debug.LogWarning($"Clip '{oldClip}' not found in AnimatorOverrideController.");
        }
    }

    public void PlayClip(string clipName)
    {
        animController.Play(clipName);
    }
}
