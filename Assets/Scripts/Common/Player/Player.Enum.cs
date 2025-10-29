using UnityEngine;

public partial class Player : MonoBehaviour
{
    public enum PlayerEvent
    {
        None,
        Idle,
        Forward,
        Backward,
        Thrush,
        Swing,
        Missile,
        Skill,
        Spell,
        Chant,
        Guard,
        Taunt,
        Damage,
        Dying,
        Dead,
        ThrustSwing,
        SwingMissile,
        SwingSkill,
        SkillSpell,
        Flash,
        Jump
    }
}
