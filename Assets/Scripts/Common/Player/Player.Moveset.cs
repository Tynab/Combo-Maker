using UnityEngine;
using static Common;
using static UnityEngine.Input;
using static UnityEngine.KeyCode;
using static UnityEngine.Time;

public partial class Player : MonoBehaviour
{
    private void Movement(int facing)
    {
        var x = GetAxis(AXIS_HORIZONTAL);
        var vector = x * facing;

        if (_movingFlag)
        {
            Forward(vector);
            Backward(vector);

            Moving(x);
        }

        Flash(x);
    }

    private void Flash(float x)
    {
        if (!_flashingFlag)
        {
            _flashVector = x > 0 ? 1 : x < 0 ? -1 : 0;
        }

        if (GetKeyDown(L) && _flashVector is not 0 && !FlashVFX.activeInHierarchy)
        {
            FlashFlagOn();
        }

        if (_flashingFlag)
        {
            Flashing(_flashVector);
        }
    }

    private void Combo(int facing)
    {
        if (GetKeyDown(J))
        {
            var animatorStateInfo = PlayerAnimator.GetCurrentAnimatorStateInfo(0);

            Thrust(animatorStateInfo);
            Swing(animatorStateInfo);
            Missile(animatorStateInfo);
            Skill(animatorStateInfo);
            Spell(animatorStateInfo);
        }

        SlidingAndFlipping(facing);
    }

    private void SSkill(int facing)
    {
        if (GetKey(I))
        {
            _chargeTime += deltaTime;
        }

        if (GetKeyUp(I))
        {
            Chant();
        }

        if (_chantingFlag)
        {
            Chanting(facing);
        }

        if (_jumpingFlag)
        {
            Jumping(facing);

            if (!_isCameraMoving)
            {
                CameraEffect.MoveUpDown(1.5f, .2f, holdAtTop: .1f);

                _isCameraMoving = true;
            }
        }

        if (_bladingFlag)
        {
            Blading(facing);

            _isCameraMoving = false;
        }
    }

    private void Forward(float vector)
    {
        if (vector > 0)
        {
            PlayerAnimator.SetBool(_forwardHash, true);
        }
        else
        {
            PlayerAnimator.SetBool(_forwardHash, false);
        }
    }

    private void Backward(float vector)
    {
        if (vector < 0)
        {
            PlayerAnimator.SetBool(_backwardHash, true);
        }
        else
        {
            PlayerAnimator.SetBool(_backwardHash, false);
        }
    }

    private void Thrust(AnimatorStateInfo animatorStateInfo)
    {
        if (animatorStateInfo.IsName(EVADE_ANIMATION_NAME) || animatorStateInfo.IsName(WAIT_ANIMATION_NAME) || animatorStateInfo.IsName(FRONTSTEP_ANIMATION_NAME))
        {
            PlayerAnimator.SetTrigger(_thrustHash);
            CameraEffect.ShakeHorizontal(1, .5f, .4f);
        }
    }

    private void Swing(AnimatorStateInfo animatorStateInfo)
    {
        if (animatorStateInfo.IsName(THRUST_ANIMATION_NAME) && animatorStateInfo.normalizedTime >= THRUST_SWING_POINT)
        {
            PlayerAnimator.SetTrigger(_swingHash);
            CameraEffect.ShakeHorizontal(1, .5f, .4f);

            SwingFlipFlagOn();
        }
    }

    private void Missile(AnimatorStateInfo animatorStateInfo)
    {
        if (animatorStateInfo.IsName(SWING_ANIMATION_NAME) && animatorStateInfo.normalizedTime >= SWING_MISSILE_POINT)
        {
            PlayerAnimator.SetTrigger(_missileHash);
            CameraEffect.ShakeVertical(1, .5f, .4f);
        }
    }

    private void Skill(AnimatorStateInfo animatorStateInfo)
    {
        if (animatorStateInfo.IsName(SWING_ANIMATION_NAME) && animatorStateInfo.normalizedTime >= SWING_SKILL_POINT)
        {
            PlayerAnimator.SetTrigger(_skillHash);
            CameraEffect.Earthquake(.5f, 1, 5, 0);
        }
    }

    private void Spell(AnimatorStateInfo animatorStateInfo)
    {
        if (animatorStateInfo.IsName(SKILL_ANIMATION_NAME) && animatorStateInfo.normalizedTime >= SKILL_SPELL_POINT)
        {
            PlayerAnimator.SetTrigger(_spellHash);
            CameraEffect.ShakeVertical(1.5f, 2, .4f);

            _slideTime = SLIDE_TIME;
        }
    }

    private void Chant()
    {
        if (_chargeTime >= 3)
        {
            PlayerAnimator.SetTrigger(_sSKillHash);
            ChargeVFX.Active();

            ChantFlagOn();

            CameraEffect.ShakeBoth(.5f, 5);
        }
    }
}
