using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private CancellationTokenSource _animationCancellationTokenSource = null;

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

        if (_animationCancellationTokenSource != null)
        {
            _animationCancellationTokenSource.Cancel();
            _animationCancellationTokenSource.Dispose();
            _animationCancellationTokenSource = null;
        }

        _animator.speed = playbackSpeed / 60f;
        
        if (startTime > 0f)
            _animator.Play(clip.name, 0, startTime / clip.length);
        else
            _animator.Play(clip.name);

        _currentClip = clip;
    }

    public async Task PlayAnimationClipAsync(AnimationClip clip, float playbackSpeed = 60f, float startTime = 0f)
    {
        if (_currentClip != null && _currentClip == clip)
            return;

        if (_animationCancellationTokenSource != null)
        {
            _animationCancellationTokenSource.Cancel();
            _animationCancellationTokenSource.Dispose();
            _animationCancellationTokenSource = null;
        }

        _animationCancellationTokenSource = new CancellationTokenSource();
        CancellationToken token = _animationCancellationTokenSource.Token;

        _animator.speed = playbackSpeed / 60f;
        
        if (startTime > 0f)
            _animator.Play(clip.name, 0, startTime / clip.length);
        else
            _animator.Play(clip.name);

        _currentClip = clip;

        try 
        {
            float clipDuration = clip.length / _animator.speed;
            await Task.Delay((int)(clipDuration * 1000), token);
        }
        catch (OperationCanceledException)
        {
            // Animation was interrupted, just exit
        }
        finally
        {
            _animationCancellationTokenSource.Dispose();
            _animationCancellationTokenSource = null;
        }

        _currentClip = null;
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
