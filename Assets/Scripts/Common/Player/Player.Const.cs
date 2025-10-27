using UnityEngine;

public partial class Player : MonoBehaviour
{
    private const string WAIT_ANIMATION_NAME = "Wait";
    private const string FRONTSTEP_ANIMATION_NAME = "Frontstep";
    private const string EVADE_ANIMATION_NAME = "Evade";
    private const string THRUST_ANIMATION_NAME = "Thrust";
    private const string SWING_ANIMATION_NAME = "Swing";
    private const string SKILL_ANIMATION_NAME = "Skill";

    private const float THRUST_SWING_POINT = .5f;
    private const float SWING_MISSILE_POINT = .5f;
    private const float SWING_SKILL_POINT = .7f;
    private const float SKILL_SPELL_POINT = .7f;

    private const float SLIDE_TIME = .7f;

    private const float FLASH_SPEED = 5;

    private const int JUMP_SPEED_RATE = 2;
}
