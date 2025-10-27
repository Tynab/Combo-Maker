using UnityEngine;

public partial class CameraEffect : MonoBehaviour
{
    public void ShakeVertical(float amp = 1, float dur = 1, float delay = 0) => StartShake(amplitude * amp, duration * dur, frequency, ShakeAxis.Vertical, delay: delay);

    public void ShakeHorizontal(float amp = 1, float dur = 1, float delay = 0) => StartShake(amplitude * amp, duration * dur, frequency, ShakeAxis.Horizontal, delay: delay);

    public void ShakeBoth(float amp = 1, float dur = 1, float delay = 0) => StartShake(amplitude * amp, duration * dur, frequency, ShakeAxis.Both, delay: delay);

    public void Earthquake(float dur = 2.5f, float amp = 1.2f, float freq = 8, int afterShockCount = 3, float delay = 0)
    {
        if (_earthquakeCoroutine is not null)
        {
            StopCoroutine(_earthquakeCoroutine);
        }

        _earthquakeCoroutine = StartCoroutine(EarthquakeRoutine(dur, amp, freq, afterShockCount, delay));
    }

    public void MoveVerticalBy(float deltaY, float duration, float delay = 0, bool resetAtEnd = false, AnimationCurve curve = null)
    {
        if (_moveCoroutine is not null)
        {
            StopCoroutine(_moveCoroutine);
        }

        _moveCoroutine = StartCoroutine(MoveVerticalRoutine(deltaY, duration, delay, resetAtEnd, curve));
    }

    public void MoveUpDown(float deltaY, float duration, float delay = 0, float holdAtTop = 0, AnimationCurve curve = null)
    {
        if (_moveCoroutine is not null)
        {
            StopCoroutine(_moveCoroutine);
        }

        _moveCoroutine = StartCoroutine(MoveUpDownRoutine(deltaY, duration, delay, holdAtTop, curve));
    }
}
