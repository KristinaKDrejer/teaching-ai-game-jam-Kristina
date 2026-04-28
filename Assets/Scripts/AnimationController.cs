using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private AnimationClip _currentClip;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimationClip(AnimationClip clip, float playbackSpeed = 60f, float startTime = 0f)
    {
        if (_currentClip != null && _currentClip == clip)
            return;

        _animator.speed = playbackSpeed / 60f;
        
        if (startTime > 0f)
            _animator.Play(clip.name, 0, startTime / clip.length);
        else
            _animator.Play(clip.name);

        _currentClip = clip;
    }

    public void PlayAnimationClip(out float time, AnimationClip clip, float playbackSpeed = 60f, float startTime = 0f)
    {
        PlayAnimationClip(clip, playbackSpeed, startTime);
        time = clip.length / _animator.speed;
    }

    public AnimationClip GetClip(string name)
    {
        if (_animator.runtimeAnimatorController == null)
        {
            Debug.LogWarning($"Animator on {gameObject.name} has no controller assigned.");
            return null;
        }

        foreach (var clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
                return clip;
        }

        Debug.LogWarning($"Clip '{name}' not found in Animator on {gameObject.name}.");
        return null;
    }

    public float GetCurrentAnimationTime()
    {
        if (_currentClip == null)
            return 0f;

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime * _currentClip.length;
    }
}
