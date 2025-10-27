using UnityEngine;

public partial class Player : MonoBehaviour
{
    [SerializeField] private float SpeedMove = 5f;

    [SerializeField] private CameraEffect CameraEffect;

    [SerializeField] private GameObject SlideObject;
    [SerializeField] private GameObject FlashObject;
    [SerializeField] private GameObject ChargeObject;
    [SerializeField] private GameObject BladeObject;

    [SerializeField] private Animator PlayerAnimator;

    [SerializeField] private Rigidbody2D Rigidbody;

    [SerializeField] private SpriteRenderer SpriteRenderer;
}
