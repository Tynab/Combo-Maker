using UnityEngine;

public partial class Player : MonoBehaviour
{
    private const string IDLE_ANIMATION_NAME = "Wait";
    private const string FORWARD_ANIMATION_NAME = "Frontstep";
    private const string BACKWARD_ANIMATION_NAME = "Evade";
    private const string THRUST_ANIMATION_NAME = "Thrust";
    private const string SWING_ANIMATION_NAME = "Swing";
    private const string MISSILE_ANIMATION_NAME = "Missile";
    private const string SKILL_ANIMATION_NAME = "Skill";
    private const string SPELL_ANIMATION_NAME = "Spell";
    private const string CHANT_ANIMATION_NAME = "Chant";
    private const string GUARD_ANIMATION_NAME = "Guard";
    private const string TAUNT_ANIMATION_NAME = "Taunt";
    private const string DAMAGE_ANIMATION_NAME = "Damage";
    private const string DYING_ANIMATION_NAME = "Dying";
    private const string DEAD_ANIMATION_NAME = "Dead";

    private const float THRUST_SWING_POINT = .5f;
    private const float SWING_MISSILE_POINT = .5f;
    private const float SWING_SKILL_POINT = .7f;
    private const float SKILL_SPELL_POINT = .7f;

    private const float SLIDE_TIME = .7f;
    private const float FLASH_TIME = .25f;
    private const float CHARGE_TIME = 3;
    private const float CHANT_TIME = 1;
    private const float JUMP_TIME = .5f;
    private const float PEAK_JUMP_TIME = JUMP_TIME / 2;
    private const float BLADE_TIME = .5f;

    private const float FLASH_SPEED = 5;

    private const int VECTOR_RIGHT = 1;
    private const int VECTOR_LEFT = -1;

    private const int JUMP_SPEED_RATE = 2;
}
