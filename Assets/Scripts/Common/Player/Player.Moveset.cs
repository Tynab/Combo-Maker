using UnityEngine;
using static Common;
using static UnityEngine.Input;
using static UnityEngine.KeyCode;
using static UnityEngine.Time;

public partial class Player : MonoBehaviour
{
    private void Movement(float x, float vector)
    {
        if (IsIn(CurrentPlayerEvent, PlayerEvent.Idle, PlayerEvent.Forward, PlayerEvent.Backward))
        {
            SetMove(vector);
            Moving(x);
        }
    }

    private void Flash(float x)
    {
        if (IsIn(CurrentPlayerEvent,
            PlayerEvent.Idle,
            PlayerEvent.Forward,
            PlayerEvent.Backward,
            PlayerEvent.Thrush,
            PlayerEvent.Swing,
            PlayerEvent.Missile,
            PlayerEvent.Skill,
            PlayerEvent.Spell,
            PlayerEvent.ThrustSwing,
            PlayerEvent.SwingMissile,
            PlayerEvent.SwingSkill,
            PlayerEvent.SkillSpell,
            PlayerEvent.Jump))
        {
            _flashVector = x > 0 ? VECTOR_RIGHT : x < 0 ? VECTOR_LEFT : 0;

            if (GetKeyDown(L) && _flashVector is not 0)
            {
                CurrentPlayerEvent = PlayerEvent.Flash;

                _flashTime = FLASH_TIME;
            }
        }

        if (CurrentPlayerEvent is PlayerEvent.Flash)
        {
            FlashVFX.Active();

            if (TickDown(ref _flashTime))
            {
                Flashing(x);
            }
            else
            {
                FlashVFX.Deactive();

                CurrentPlayerEvent = _flashVector > 0 ? PlayerEvent.Forward : _flashVector < 0 ? PlayerEvent.Backward : PlayerEvent.Idle;

                _flashVector = 0;
            }
        }
    }

    private void Combo(int facing)
    {
        if (GetKeyDown(J))
        {
            Thrust();
            Swing();
            Missile();
            Skill();
            Spell();
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

        if (CurrentPlayerEvent is PlayerEvent.Chant)
        {
            Chanting();
        }

        if (CurrentPlayerEvent is PlayerEvent.Jump)
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

    private void SetMove(float vector)
    {
        PlayerAnimator.SetBool(_forwardHash, vector > 0);
        PlayerAnimator.SetBool(_backwardHash, vector < 0);
    }

    private void Thrust()
    {
        if (IsIn(CurrentPlayerEvent, PlayerEvent.Idle, PlayerEvent.Forward, PlayerEvent.Backward))
        {
            PlayerAnimator.SetTrigger(_thrustHash);
            CameraEffect.ShakeHorizontal(1, .5f, .4f);
        }
    }

    private void Swing()
    {
        if (CurrentPlayerEvent is PlayerEvent.ThrustSwing)
        {
            PlayerAnimator.SetTrigger(_swingHash);
            CameraEffect.ShakeHorizontal(1, .5f, .4f);

            _slideTime = SLIDE_TIME;
            _swingFlipingFlag = true;
        }
    }

    private void Missile()
    {
        if (CurrentPlayerEvent is PlayerEvent.SwingMissile)
        {
            PlayerAnimator.SetTrigger(_missileHash);
            CameraEffect.ShakeVertical(1, .5f, .4f);
        }
    }

    private void Skill()
    {
        if (CurrentPlayerEvent is PlayerEvent.SwingSkill)
        {
            PlayerAnimator.SetTrigger(_skillHash);
            CameraEffect.Earthquake(.5f, 1, 5, 0);
        }
    }

    private void Spell()
    {
        if (CurrentPlayerEvent is PlayerEvent.SkillSpell)
        {
            PlayerAnimator.SetTrigger(_spellHash);
            CameraEffect.ShakeVertical(1.5f, 2, .4f);

            _slideTime = SLIDE_TIME;
        }
    }

    private void Chant()
    {
        if (_chargeTime >= CHARGE_TIME)
        {
            PlayerAnimator.SetTrigger(_sSKillHash);
            ChargeVFX.Active();

            CurrentPlayerEvent = PlayerEvent.Chant;

            _chargeTime = 0;
            _chantTime = CHANT_TIME;

            CameraEffect.ShakeBoth(.5f, 5);
        }
    }
}
