using UnityEngine;
using static UnityEngine.Time;

public partial class Player : MonoBehaviour
{
    private void Moving(float x) => _rb2d.linearVelocityX = x * SpeedMove;

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

                _swingFlipingFlag = false;
            }
        }
    }

    private void Flashing(float x)
    {
        FlashVFX.Active();

        if (_flashTime > 0)
        {
            _flashTime -= deltaTime;

            transform.position = new Vector3(transform.position.x + x / FLASH_SPEED, transform.position.y);
        }
        else
        {
            FlashVFX.Deactive();

            _flashVector = 0;
            _flashingFlag = false;
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
            _chantingFlag = false;

            ChargeVFX.Deactive();

            _jumpTime = .5f;
            _peakJumpTime = .25f;
            _jumpingFlag = true;
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
            _jumpingFlag = false;

            CameraEffect.ShakeVertical(3, 2);
            CameraEffect.Earthquake(.5f, .5f, 5, 0);
            BladeVFX.Active();

            _bladeTime = .5f;
            _bladingFlag = true;
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
            _bladingFlag = false;

            BladeVFX.Deactive();
            BladeVFX.ResetPositionAndRotation(new Vector3(0, .8f));
        }
    }
}
