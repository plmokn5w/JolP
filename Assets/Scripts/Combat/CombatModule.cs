
using UnityEngine;

public class CombatModule : MonoBehaviour
{
    [field: SerializeField] public float HitstunTimer = 0.0f;

    public void SetHitstun(float time)
    {
        HitstunTimer = time;
    }

    public void AddHitstun(float time)
    {
        HitstunTimer += time;
    }

    private void Update()
    {
        HitstunTimer = Mathf.MoveTowards(HitstunTimer, 0f, Time.deltaTime);
    }
}