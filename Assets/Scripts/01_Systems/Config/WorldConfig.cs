
using UnityEngine;

public class WorldConfig : MonoBehaviour
{
    public static WorldConfig Instance;

    [field: SerializeField] public float ZtoY { get; private set; }
    [field: SerializeField] public float Gravity { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
