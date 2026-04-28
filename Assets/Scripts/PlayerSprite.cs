using UnityEngine;

[RequireComponent(typeof(AnimationController))]
public class PlayerSprite : MonoBehaviour
{
    [SerializeField] private AnimationClip _idleAnimation;
    [SerializeField] private AnimationClip _runAnimation;

    private AnimationController _animationController;

    private void Awake()
    {
        _animationController = GetComponent<AnimationController>();
    }

    private void SetWalking(Vector2 direction)
    {
        if (direction.magnitude == 0)
        {
            _animationController.PlayAnimationClip(_idleAnimation);
            return;
        }
        
        float xScale = direction.x < 0 ? -1 : 1;
        transform.localScale = new Vector3(xScale, 1, 1);
        _animationController.PlayAnimationClip(_runAnimation);
    }
}
