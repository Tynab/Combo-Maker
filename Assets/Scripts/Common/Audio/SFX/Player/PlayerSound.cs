using UnityEngine;

public partial class PlayerSound : MonoBehaviour
{
    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void OnThrust() => _audioSource.PlayOneShot(ThrustSFX);

    public void OnSwing() => _audioSource.PlayOneShot(SwingSFX);

    public void OnMissile() => _audioSource.PlayOneShot(MissileSFX);

    public void OnSkill() => _audioSource.PlayOneShot(SkillSFX);

    public void OnSpell() => _audioSource.PlayOneShot(SpellSFX);

    public void OnTaunt() => _audioSource.PlayOneShot(TauntSFX);
}
