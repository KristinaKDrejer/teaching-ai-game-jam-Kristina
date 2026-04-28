using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float _cooldownTime = 0.25f;
    private float _lastAttacked = 0;
    private AnimationController _animationController;

    [SerializeField] private AnimationClip _attackAnimation;

    private void Awake()
    {
        _animationController = GetComponent<AnimationController>();
    }

    private void Update()
    {
        _lastAttacked += Time.deltaTime;
    }

    private async void Attack()
    {
        if (_lastAttacked < _cooldownTime) return;

        _lastAttacked = 0;
        _animationController.PlayAnimationClip(out float time, _attackAnimation);
        _lastAttacked = -time;
    }
}
