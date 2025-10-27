using UnityEngine;

public partial class BladeSound : MonoBehaviour
{
    private void Awake() => _audioSource = GetComponent<AudioSource>();

    public void OnBlade() => _audioSource.PlayOneShot(BladeSFX);
}
