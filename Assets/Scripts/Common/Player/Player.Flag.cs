using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    private void FlashFlagOn()
    {
        _flashTime = .25f;
        _flashingFlag = true;
        _movingFlag = false;
    }

    private void FlashFlagOff()
    {
        _flashVector = 0;
        _flashingFlag = false;
        _movingFlag = true;
    }

    private void SwingFlipFlagOn()
    {
        _slideTime = SLIDE_TIME;
        _swingFlipingFlag = true;
    }

    private void SwingFlipFlagOff() => _swingFlipingFlag = false;

    private void ChantFlagOn()
    {
        _chargeTime = 0;
        _chantTime = 1;
        _chantingFlag = true;
        _movingFlag = false;
    }

    private void ChantFlagOff() => _chantingFlag = false;

    private void JumpFlagOn()
    {
        _jumpTime = .5f;
        _peakJumpTime = .25f;
        _jumpingFlag = true;
    }

    private void JumpFlagOff()
    {
        _jumpingFlag = false;
        _movingFlag = true;
    }

    private void BladeFlagOn()
    {
        _bladeTime = .5f;
        _bladingFlag = true;
    }

    private void BladeFlagOff() => _bladingFlag = false;
}
