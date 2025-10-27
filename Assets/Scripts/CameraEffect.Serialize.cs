using UnityEngine;
using static UnityEngine.AnimationCurve;

public partial class CameraEffect : MonoBehaviour
{
    [SerializeField] private AnimationCurve envelope = EaseInOut(0, 1, 1, 0);

    [SerializeField] private float amplitude = .3f;
    [SerializeField] private float duration = .25f;
    [SerializeField] private float frequency = 25f;
    [SerializeField] private float rotationAmplitudeDegree = 1.5f;

    [SerializeField] private bool resetRotationOnStop = true;
}
