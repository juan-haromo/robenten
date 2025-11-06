using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditor.Playables;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    public Animator animController;

    public AnimatorOverrideController overrideController;

    void Awake()
    {
        // Create and assign one override controller that wraps the base controller
        overrideController = new AnimatorOverrideController(animController.runtimeAnimatorController);
        animController.runtimeAnimatorController = overrideController;
    }

    public void ReplaceClip(string originalClipName, AnimationClip newClip)
    {
        if (newClip == null)
        {
            Debug.LogWarning("New clip is null.");
            return;
        }

        // Get current overrides
        var overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        overrideController.GetOverrides(overrides);

        bool replaced = false;

        for (int i = 0; i < overrides.Count; i++)
        {
            var pair = overrides[i];
            if (pair.Key != null && pair.Key.name == originalClipName)
            {
                overrides[i] = new KeyValuePair<AnimationClip, AnimationClip>(pair.Key, newClip);
                replaced = true;
                break;
            }
        }

        if (replaced)
        {
            overrideController.ApplyOverrides(overrides);
            Debug.Log($"Replaced clip '{originalClipName}' with '{newClip.name}'.");
        }
        else
        {
            Debug.LogWarning($"Clip '{originalClipName}' not found in AnimatorController.");
        }
    }

    public void PlayClip(string clipName)
    {
        animController.Play(clipName);
    }
}
