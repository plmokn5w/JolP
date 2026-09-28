
using UnityEngine;
using System.Collections.Generic;

public class RenderModule : Module
{
    private Transform transform;
    private Transform visualTransform;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Dictionary<StateKey, string> states;

    public override void Initialize(ModuleFrame frame)
    {
        base.Initialize(frame);
        transform = frame.transform;
        visualTransform = transform.GetChild(0);
        if (!visualTransform.TryGetComponent(out spriteRenderer))
            Debug.Log("자식오브젝트에 스프라이트렌더러 컴포넌트가 없는데스웅;;");
        if (!visualTransform.TryGetComponent(out animator))
            Debug.Log("자식오브젝트에 애니메이터 컴포넌트가 없는데스웅;;");
    }

    public void Initialize(RenderModuleData data)
    {
        states = new Dictionary<StateKey, string>();
        for (int i = 0; i < data.states.Count; i++)
            states.Add(data.states[i].key, data.states[i].name);
    }

    public void OnLateUpdate()
    {
        float yOffset = transform.position.z * WorldConfig.Instance.ZtoY;
        visualTransform.position = new Vector3(0f, yOffset, 0f);
    }

    public void Lookat(FacingDirection dir)
    {
        switch (dir)
        {
            case FacingDirection.Left:
                spriteRenderer.flipX = true;
                break;
            case FacingDirection.Right:
                spriteRenderer.flipX = false;
                break;
        }
    }

    public void RenderState(StateKey key) { animator.Play(states[key]); }
}
