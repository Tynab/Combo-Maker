using UnityEngine;
using static UnityEngine.AnimationCurve;

[DisallowMultipleComponent]
public partial class CameraEffect : MonoBehaviour
{
    private void Awake()
    {
        _baseLocalPosition = transform.localPosition;
        _baseLocalRotation = transform.localRotation;

        if (envelope is null || envelope.length is 0)
        {
            envelope = EaseInOut(0, 1, 1, 0);
        }
    }

    private void OnDisable()
    {
        transform.localPosition = _baseLocalPosition;

        if (resetRotationOnStop)
        {
            transform.localRotation = _baseLocalRotation;
        }

        if (_shakeCoroutine is not null)
        {
            StopCoroutine(_shakeCoroutine);

            _shakeCoroutine = null;
        }

        if (_earthquakeCoroutine is not null)
        {
            StopCoroutine(_earthquakeCoroutine);

            _earthquakeCoroutine = null;
        }

        if (_moveCoroutine is not null)
        {
            StopCoroutine(_moveCoroutine);

            _moveCoroutine = null;
        }
    }
}
