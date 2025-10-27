using System.Collections;
using UnityEngine;
using static UnityEngine.AnimationCurve;
using static UnityEngine.Mathf;
using static UnityEngine.Quaternion;
using static UnityEngine.Random;
using static UnityEngine.Time;
using static UnityEngine.Vector3;

public partial class CameraEffect : MonoBehaviour
{
    private IEnumerator ShakeRoutine(float amp, float dur, float freq, ShakeAxis axis, float rotAmpDeg, float delay)
    {
        if (delay > 0f)
        {
            yield return new WaitForSecondsRealtime(delay);
        }

        transform.SetLocalPositionAndRotation(_baseLocalPosition, _baseLocalRotation);

        var t = 0f;
        var seedX = Range(0f, 1_000);
        var seedY = Range(0f, 1_000);
        var seedR = Range(0f, 1_000);

        while (t < dur)
        {
            var env = envelope.Evaluate(dur > 0 ? t / dur : 1);
            var p = _baseLocalPosition;
            var nx = PerlinNoise(seedX, time * freq) * 2 - 1;
            var ny = PerlinNoise(seedY, time * freq) * 2 - 1;

            if (axis is ShakeAxis.Horizontal or ShakeAxis.Both)
            {
                p.x += nx * amp * env;
            }

            if (axis is ShakeAxis.Vertical or ShakeAxis.Both)
            {
                p.y += ny * amp * env;
            }

            transform.localPosition = p;

            if (rotAmpDeg > 0)
            {
                transform.localRotation = _baseLocalRotation * Euler(0, 0, (PerlinNoise(seedR, time * (freq * .7f)) * 2 - 1) * rotAmpDeg * env);
            }

            t += unscaledDeltaTime;

            yield return null;
        }

        transform.localPosition = _baseLocalPosition;

        if (resetRotationOnStop)
        {
            transform.localRotation = _baseLocalRotation;
        }

        _shakeCoroutine = null;
    }

    private IEnumerator EarthquakeRoutine(float dur, float amp, float freq, int afterShockCount, float delay)
    {
        if (delay > 0f)
        {
            yield return new WaitForSecondsRealtime(delay);
        }

        var warmUp = dur * .2f;
        var main = dur * .5f;
        var cool = dur * .3f;
        var rise = EaseInOut(0, 0, 1, 1);
        var fall = EaseInOut(0, 1, 1, 0);

        yield return StartCoroutine(DriveAmpOverTime(warmUp, amp * .5f, freq * .9f, rise));

        yield return StartCoroutine(ShakeRoutine(amp, main, freq, ShakeAxis.Both, Max(rotationAmplitudeDegree, 1), delay));

        yield return StartCoroutine(DriveAmpOverTime(cool, amp * .5f, freq * .8f, fall));

        for (var i = 0; i < afterShockCount; i++)
        {
            yield return StartCoroutine(ShakeRoutine(amp * Range(.25f, .5f), Range(.12f, .25f), freq * Range(.8f, 1.2f), ShakeAxis.Both, rotationAmplitudeDegree * .6f, delay));

            yield return new WaitForSecondsRealtime(Range(.06f, .18f));
        }

        _earthquakeCoroutine = null;
    }

    private IEnumerator MoveVerticalRoutine(float deltaY, float duration, float delay, bool resetAtEnd, AnimationCurve curve)
    {
        if (delay > 0)
        {
            yield return new WaitForSecondsRealtime(delay);
        }

        if (duration <= 0 || Abs(deltaY) <= 0)
        {
            yield break;
        }

        var c = curve ?? EaseInOut(0, 0, 1, 1);
        var startBase = _baseLocalPosition;
        var targetBase = startBase + new Vector3(0f, deltaY, 0f);
        var t = 0f;

        while (t < duration)
        {
            _baseLocalPosition = Lerp(startBase, targetBase, c.Evaluate(t / duration));

            if (_shakeCoroutine is null)
            {
                transform.localPosition = _baseLocalPosition;
            }

            t += unscaledDeltaTime;

            yield return null;
        }

        _baseLocalPosition = targetBase;

        if (_shakeCoroutine is null)
        {
            transform.localPosition = _baseLocalPosition;
        }

        if (resetAtEnd)
        {
            t = 0;

            var backStart = _baseLocalPosition;
            var backTarget = startBase;

            while (t < duration)
            {
                _baseLocalPosition = Lerp(backStart, backTarget, c.Evaluate(t / duration));

                if (_shakeCoroutine is null)
                {
                    transform.localPosition = _baseLocalPosition;
                }

                t += unscaledDeltaTime;

                yield return null;
            }

            _baseLocalPosition = backTarget;

            if (_shakeCoroutine is null)
            {
                transform.localPosition = _baseLocalPosition;
            }
        }

        _moveCoroutine = null;
    }

    private IEnumerator MoveUpDownRoutine(float deltaY, float duration, float delay, float holdAtTop, AnimationCurve curve)
    {
        yield return StartCoroutine(MoveVerticalRoutine(deltaY, duration, delay, resetAtEnd: false, curve));

        if (holdAtTop > 0)
        {
            yield return new WaitForSecondsRealtime(holdAtTop);
        }

        yield return StartCoroutine(MoveVerticalRoutine(-deltaY, duration, 0, resetAtEnd: false, curve));

        _moveCoroutine = null;
    }

    private void StartShake(float amp, float dur, float freq, ShakeAxis axis = ShakeAxis.Vertical, float rotAmpDeg = 0, float delay = 0)
    {
        if (_shakeCoroutine is not null)
        {
            StopCoroutine(_shakeCoroutine);
        }

        _shakeCoroutine = StartCoroutine(ShakeRoutine(amp, dur, freq, axis, rotAmpDeg, delay));
    }

    private IEnumerator DriveAmpOverTime(float dur, float targetAmp, float freq, AnimationCurve curve01)
    {
        if (dur <= 0f)
        {
            yield break;
        }

        var t = 0f;
        var seedX = Range(0f, 1_000);
        var seedY = Range(0f, 1_000);
        var seedR = Range(0f, 1_000);

        while (t < dur)
        {
            var env = curve01.Evaluate(t / dur);
            var amp = targetAmp * env;
            var p = _baseLocalPosition;
            var nx = PerlinNoise(seedX, time * freq) * 2 - 1;
            var ny = PerlinNoise(seedY, time * freq) * 2 - 1;

            p.x += nx * amp;
            p.y += ny * amp;

            transform.localPosition = p;

            if (rotationAmplitudeDegree > 0f)
            {
                transform.localRotation = _baseLocalRotation * Euler(0f, 0f, (PerlinNoise(seedR, time * (freq * .7f)) * 2 - 1) * rotationAmplitudeDegree * env);
            }

            t += unscaledDeltaTime;

            yield return null;
        }
    }
}
