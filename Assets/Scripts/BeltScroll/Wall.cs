
using UnityEngine;

public class Wall : MonoBehaviour
{
    private Stage stage;
    [SerializeField] private float xDir;

    private void Awake()
    {
        stage = GetComponentInParent<Stage>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Bounceable bounceable))
            stage.OnBoundaryTriggered(bounceable, xDir);
    }
}
