
using UnityEngine;

public class RenderModule : MonoBehaviour
{
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    public void Lookat(FacingSide facingSide)
    {
        if (facingSide == FacingSide.Left)
            spriteRenderer.flipX = true;
        if (facingSide == FacingSide.Right)
            spriteRenderer.flipX = false;
    }

    public void RenderState(StateKey state)
    {
        animator.Play(WorldConfig.Instance.AnimationHash[state]);
        animator.Update(0f);
    }

    public bool IsAnimationFinished()
    {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return !animatorStateInfo.loop && animatorStateInfo.normalizedTime >= 1.0f;
    }

    private void LateUpdate()
    {
        float yOffset = transform.position.z * WorldConfig.Instance.ZtoY;

        spriteTransform.localPosition = Vector3.up * yOffset;
    }
}