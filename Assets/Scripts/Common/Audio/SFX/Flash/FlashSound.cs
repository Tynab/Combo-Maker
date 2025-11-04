using UnityEngine;

public partial class FlashSound : MonoBehaviour
{
    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void OnFlash() => _audioSource.PlayOneShot(FlashSFX);
}
