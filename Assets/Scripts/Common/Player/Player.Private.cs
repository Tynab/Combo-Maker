using UnityEngine;
using static UnityEngine.Time;

public partial class Player : MonoBehaviour
{
    private void GetPlayerEvent(AnimatorStateInfo animatorStateInfo) => CurrentPlayerEvent = animatorStateInfo.IsName(IDLE_ANIMATION_NAME)
        ? PlayerEvent.Idle
        : animatorStateInfo.IsName(FORWARD_ANIMATION_NAME)
        ? PlayerEvent.Forward
        : animatorStateInfo.IsName(BACKWARD_ANIMATION_NAME)
        ? PlayerEvent.Backward
        : animatorStateInfo.IsName(THRUST_ANIMATION_NAME)
            ? animatorStateInfo.normalizedTime >= THRUST_SWING_POINT ? PlayerEvent.ThrustSwing : PlayerEvent.Thrush
        : animatorStateInfo.IsName(SWING_ANIMATION_NAME)
            ? animatorStateInfo.normalizedTime >= SWING_SKILL_POINT ? PlayerEvent.SwingSkill
            : animatorStateInfo.normalizedTime >= SWING_MISSILE_POINT ? PlayerEvent.SwingMissile : PlayerEvent.Swing
        : animatorStateInfo.IsName(SKILL_ANIMATION_NAME)
            ? animatorStateInfo.normalizedTime >= SKILL_SPELL_POINT ? PlayerEvent.SkillSpell : PlayerEvent.Skill
        : animatorStateInfo.IsName(SPELL_ANIMATION_NAME)
        ? PlayerEvent.Spell
        : animatorStateInfo.IsName(CHANT_ANIMATION_NAME)
        ? PlayerEvent.Chant
        : animatorStateInfo.IsName(GUARD_ANIMATION_NAME)
        ? PlayerEvent.Guard
        : animatorStateInfo.IsName(TAUNT_ANIMATION_NAME)
        ? PlayerEvent.Taunt
        : animatorStateInfo.IsName(DAMAGE_ANIMATION_NAME)
        ? PlayerEvent.Damage
        : animatorStateInfo.IsName(DYING_ANIMATION_NAME)
        ? PlayerEvent.Dying
        : animatorStateInfo.IsName(DEAD_ANIMATION_NAME) ? PlayerEvent.Dead : PlayerEvent.None;

    private void Moving(float x) => _rb2d.linearVelocityX = x * SpeedMove;

    private void Flashing(float x) => transform.position = new Vector3(transform.position.x + x / FLASH_SPEED, transform.position.y);

    private int Facing() => PlayerSR.flipX ? 1 : -1;

    private void Flipping()
    {
        PlayerSR.flipX = !PlayerSR.flipX;

        _slideSr.flipX = !_slideSr.flipX;
        _flashSr.flipX = !_flashSr.flipX;
        _chargeSr.flipX = !_chargeSr.flipX;
        _bladeSr.flipX = !_bladeSr.flipX;
    }

    private void Sliding(int facing)
    {
        if (_swingFlipingFlag)
        {
            SlideVFX.Active();
        }

        _rb2d.linearVelocityX = SpeedMove * facing;
    }

    private void SlidingAndFlipping(int facing)
    {
        if (_slideTime > 0)
        {
            Sliding(facing);

            _slideTime -= deltaTime;
        }
        else
        {
            SlideVFX.Deactive();

            if (_swingFlipingFlag)
            {
                Flipping();
                SwingFlipFlagOff();
            }
        }
    }

    private void Chanting(int facing)
    {
        if (_chantTime > 0)
        {
            _chantTime -= deltaTime;
        }
        else
        {
            ChantFlagOff();

            ChargeVFX.Deactive();

            JumpFlagOn();
        }
    }

    private void Jumping(int facing)
    {
        if (_jumpTime > 0)
        {
            _rb2d.linearVelocityX = JUMP_SPEED_RATE * SpeedMove * facing;
            _rb2d.linearVelocityY = _jumpTime >= _peakJumpTime ? SpeedMove : -SpeedMove;

            _jumpTime -= deltaTime;
        }
        else
        {
            JumpFlagOff();

            CameraEffect.ShakeVertical(3, 2);
            CameraEffect.Earthquake(.5f, .5f, 5, 0);
            BladeVFX.Active();

            BladeFlagOn();
        }
    }

    private void Blading(int facing)
    {
        if (_bladeTime > 0)
        {
            BladeVFX.transform.position = new Vector3(BladeVFX.transform.position.x + facing / FLASH_SPEED, BladeVFX.transform.position.y);

            _bladeTime -= deltaTime;
        }
        else
        {
            BladeFlagOff();

            BladeVFX.Deactive();
            BladeVFX.ResetPositionAndRotation(new Vector3(0, .8f));
        }
    }
}
