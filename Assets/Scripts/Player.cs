using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public partial class Player : MonoBehaviour
{
    private void Start()
    {
        _slideSr = SlideObject.GetComponent<SpriteRenderer>();
        _flashSr = SlideObject.GetComponent<SpriteRenderer>();
        _chargeSr = ChargeObject.GetComponent<SpriteRenderer>();
        _bladeSr = BladeObject.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        var facing = Facing();

        Movement(facing);
        Combo(facing);
        SSkill(facing);
    }
}
