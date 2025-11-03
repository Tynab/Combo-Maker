using UnityEngine;

public partial class Player : MonoBehaviour
{
    [SerializeField] private float SpeedMove = 5f;

    [SerializeField] private CameraEffect CameraEffect;

    [SerializeField] private GameObject SlideVFX;
    [SerializeField] private GameObject FlashVFX;
    [SerializeField] private GameObject ChargeVFX;
    [SerializeField] private GameObject BladeVFX;

    [SerializeField] private Animator PlayerAnimator;

    [SerializeField] private SpriteRenderer PlayerSR;
}
