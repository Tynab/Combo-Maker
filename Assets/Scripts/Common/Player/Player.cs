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

        if (CurrentPlayerEvent is not PlayerEvent.Slide and not PlayerEvent.Flash and not PlayerEvent.Jump)
        {
            GetPlayerEvent(animatorStateInfo);
        }

        var x = GetAxis(AXIS_HORIZONTAL);
        var facing = Facing();
        var vector = x * facing;

        if (CurrentPlayerEvent is PlayerEvent.Idle or PlayerEvent.Forward or PlayerEvent.Backward)
        {
            Movement(x, vector);
        }

        if (CurrentPlayerEvent is PlayerEvent.Idle
            or PlayerEvent.Forward
            or PlayerEvent.Backward
            or PlayerEvent.Thrush
            or PlayerEvent.Swing
            or PlayerEvent.Missile
            or PlayerEvent.Skill
            or PlayerEvent.Spell
            or PlayerEvent.ThrustSwing
            or PlayerEvent.SwingMissile
            or PlayerEvent.SwingSkill
            or PlayerEvent.SkillSpell
            or PlayerEvent.Jump)
        {
            FlashCheck(x);
        }

        if (CurrentPlayerEvent is PlayerEvent.Flash)
        {
            Flash(_flashVector);
        }

        Combo(facing);
        SSkill(facing);
    }
}
