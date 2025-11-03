using UnityEngine;
using static Common;
using static UnityEngine.Space;
using static UnityEngine.Vector3;

public partial class Player : MonoBehaviour
{
    private void GetPlayerEvent(AnimatorStateInfo animatorStateInfo)
    {
        if (CurrentPlayerEvent.IsNotIn(PlayerEvent.Flash, PlayerEvent.Jump))
        {
            CurrentPlayerEvent = animatorStateInfo.IsName(IDLE_ANIMATION_NAME)
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
                : animatorStateInfo.IsName(MISSILE_ANIMATION_NAME)
                ? PlayerEvent.Missile
                : animatorStateInfo.IsName(SKILL_ANIMATION_NAME)
                    ? animatorStateInfo.normalizedTime >= SKILL_SPELL_POINT ? PlayerEvent.SkillSpell : PlayerEvent.Skill
                : animatorStateInfo.IsName(SPELL_ANIMATION_NAME)
                ? PlayerEvent.Spell
                : animatorStateInfo.IsName(CHANT_ANIMATION_NAME)
                    ? animatorStateInfo.normalizedTime >= 1 ? PlayerEvent.Jump : PlayerEvent.Chant
                : animatorStateInfo.IsName(GUARD_ANIMATION_NAME)
                ? PlayerEvent.Guard
                : animatorStateInfo.IsName(TAUNT_ANIMATION_NAME)
                ? PlayerEvent.Taunt
                : animatorStateInfo.IsName(DAMAGE_ANIMATION_NAME)
                ? PlayerEvent.Damage
                : animatorStateInfo.IsName(DYING_ANIMATION_NAME)
                ? PlayerEvent.Dying
                : animatorStateInfo.IsName(DEAD_ANIMATION_NAME) ? PlayerEvent.Dead : PlayerEvent.None;
        }
    }

    private void Moving(float x) => _rb2d.linearVelocityX = x * SpeedMove;

    private void Flashing(float x) => transform.Translate(right * (x / FLASH_SPEED), World);

    private int Facing() => PlayerSR.flipX ? VECTOR_RIGHT : VECTOR_LEFT;

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
        if (TickDown(ref _slideTime))
        {
            Sliding(facing);
        }
        else
        {
            SlideVFX.Deactive();

            if (_swingFlipingFlag)
            {
                Flipping();

                _swingFlipingFlag = false;
            }
        }
    }

    private void Chanting()
    {
        if (!TickDown(ref _chantTime))
        {
            ChargeVFX.Deactive();

            CurrentPlayerEvent = PlayerEvent.Jump;

            _jumpTime = JUMP_TIME;
            _peakJumpTime = PEAK_JUMP_TIME;
        }
    }

    private void Jumping(int facing)
    {
        if (TickDown(ref _jumpTime))
        {
            _rb2d.linearVelocityX = JUMP_SPEED_RATE * SpeedMove * facing;
            _rb2d.linearVelocityY = _jumpTime >= _peakJumpTime ? SpeedMove : -SpeedMove;
        }
        else
        {
            CurrentPlayerEvent = PlayerEvent.Spell;

            CameraEffect.ShakeVertical(3, 2);
            CameraEffect.Earthquake(.5f, .5f, 5, 0);
            BladeVFX.Active();

            _bladeTime = BLADE_TIME;
            _bladingFlag = true;
        }
    }

    private void Blading(int facing)
    {
        if (TickDown(ref _bladeTime))
        {
            BladeVFX.transform.Translate(right * (facing / FLASH_SPEED), World);
        }
        else
        {
            _bladingFlag = false;

            BladeVFX.Deactive();
            BladeVFX.ResetPositionAndRotation(new Vector3(0, .8f));
        }
    }
}
