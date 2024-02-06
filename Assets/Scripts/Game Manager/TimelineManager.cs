using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [Header("Animator Settings")]
    public Animator playerAnimator;
    private RuntimeAnimatorController playerAnim;

    [Header("Playable Director Settings")]
    public PlayableDirector director;

    private bool fix = false;

    void OnEnable()
    {
        playerAnim = playerAnimator.runtimeAnimatorController;
        playerAnimator.runtimeAnimatorController = null;
    }

    void Update()
    {
        if (director.state != PlayState.Playing && !fix)
        {
            fix = true;
            playerAnimator.runtimeAnimatorController = playerAnim;
        }
    }
}