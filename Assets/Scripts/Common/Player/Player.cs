using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public partial class Player : MonoBehaviour
{
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();

        _slideSr = SlideVFX.GetComponent<SpriteRenderer>();
        _flashSr = FlashVFX.GetComponent<SpriteRenderer>();
        _chargeSr = ChargeVFX.GetComponent<SpriteRenderer>();
        _bladeSr = BladeVFX.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        var facing = Facing();

        Movement(facing);
        Combo(facing);
        SSkill(facing);
    }
}
