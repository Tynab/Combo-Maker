using UnityEngine;
using static Common;
using static UnityEngine.Input;

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
        var animatorStateInfo = PlayerAnimator.GetCurrentAnimatorStateInfo(0);

        GetPlayerEvent(animatorStateInfo);

        var x = GetAxis(AXIS_HORIZONTAL);
        var facing = Facing();
        var vector = x * facing;

        Movement(x, vector);
        Flash(x);
        Taunt();
        Guard();
        Combo(facing);
        SSkill(facing);
    }
}
