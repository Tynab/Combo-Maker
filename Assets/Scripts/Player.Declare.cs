using UnityEngine;
using static UnityEngine.Animator;

public partial class Player : MonoBehaviour
{
    private static readonly int _forwardHash = StringToHash("forward");
    private static readonly int _backwardHash = StringToHash("backward");
    private static readonly int _thrustHash = StringToHash("attack1");
    private static readonly int _swingHash = StringToHash("attack2");
    private static readonly int _skillHash = StringToHash("attack3a");
    private static readonly int _missileHash = StringToHash("attack3b");
    private static readonly int _spellHash = StringToHash("attack4");
    private static readonly int _sSKillHash = StringToHash("attack");

    private SpriteRenderer _slideSr;
    private SpriteRenderer _flashSr;
    private SpriteRenderer _chargeSr;
    private SpriteRenderer _bladeSr;

    private float _flashTime = 0;
    private float _slideTime = 0;
    private float _chargeTime = 0;
    private float _chantTime = 0;
    private float _jumpTime = 0;
    private float _peakJumpTime = 0;
    private float _bladeTime = 0;

    private int _flashVector = 0;

    private bool _isCameraMoving = false;

    private bool _flashingFlag = false;
    private bool _swingFlipingFlag = false;
    private bool _chantingFlag = false;
    private bool _jumpingFlag = false;
    private bool _bladingFlag = false;
}
