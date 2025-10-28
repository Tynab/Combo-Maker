using UnityEngine;

public partial class Player : MonoBehaviour
{
    public enum PlayerEvent
    {
        None = 0,
        Idle = 1,
        Forward = 2,
        Backward = 3,
        Thrush = 4,
        Swing = 5,
        Missile = 7,
        Skill = 8,
        Spell = 9,
        Chant = 10,
        Guard = 11,
        Taunt = 12,
        Damage = 13,
        Dying = 14,
        Dead = 15,
        ThrustSwing = 16,
        SwingMissile = 17,
        SwingSkill = 18,
        SkillSpell = 19,
        Slide = 20,
        Flash = 21,
        Jump = 22
    }
}
