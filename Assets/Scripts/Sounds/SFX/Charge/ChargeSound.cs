using UnityEngine;

public partial class ChargeSound : MonoBehaviour
{
    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void OnCharge() => _audioSource.PlayOneShot(ChargeSFX);
}
