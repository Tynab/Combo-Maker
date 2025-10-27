using UnityEngine;
using static UnityEngine.Time;

public partial class Player : MonoBehaviour
{
    private void Moving(float x) => Rigidbody.linearVelocityX = x * SpeedMove;

    private int Facing() => SpriteRenderer.flipX ? 1 : -1;

    private void Flipping()
    {
        SpriteRenderer.flipX = !SpriteRenderer.flipX;

        _slideSr.flipX = !_slideSr.flipX;
        _flashSr.flipX = !_flashSr.flipX;
        _chargeSr.flipX = !_chargeSr.flipX;
        _bladeSr.flipX = !_bladeSr.flipX;
    }

    private void Sliding(int facing)
    {
        if (_swingFlipingFlag)
        {
            SlideObject.Active();
        }

        Rigidbody.linearVelocityX = SpeedMove * facing;
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
            SlideObject.Deactive();

            if (_swingFlipingFlag)
            {
                Flipping();

                _swingFlipingFlag = false;
            }
        }
    }

    private void Flashing(float x)
    {
        FlashObject.Active();

        if (_flashTime > 0)
        {
            _flashTime -= deltaTime;

            transform.position = new Vector3(transform.position.x + x / FLASH_SPEED, transform.position.y);
        }
        else
        {
            FlashObject.Deactive();

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

            ChargeObject.Deactive();

            _jumpTime = .5f;
            _peakJumpTime = .25f;
            _jumpingFlag = true;
        }
    }

    private void Jumping(int facing)
    {
        if (_jumpTime > 0)
        {
            Rigidbody.linearVelocityX = JUMP_SPEED_RATE * SpeedMove * facing;
            Rigidbody.linearVelocityY = _jumpTime >= _peakJumpTime ? SpeedMove : -SpeedMove;

            _jumpTime -= deltaTime;
        }
        else
        {
            _jumpingFlag = false;

            CameraEffect.ShakeVertical(3, 2);
            CameraEffect.Earthquake(.5f, .5f, 5, 0);
            BladeObject.Active();

            _bladeTime = .5f;
            _bladingFlag = true;
        }
    }

    private void Blading(int facing)
    {
        if (_bladeTime > 0)
        {
            BladeObject.transform.position = new Vector3(BladeObject.transform.position.x + facing / FLASH_SPEED, BladeObject.transform.position.y);

            _bladeTime -= deltaTime;
        }
        else
        {
            _bladingFlag = false;

            BladeObject.Deactive();
            BladeObject.ResetPositionAndRotation(new Vector3(0, .8f));
        }
    }
}
