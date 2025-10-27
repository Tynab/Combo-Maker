using UnityEngine;

public partial class CameraEffect : MonoBehaviour
{
    private Coroutine _shakeCoroutine;
    private Coroutine _earthquakeCoroutine;
    private Coroutine _moveCoroutine;

    private Vector3 _baseLocalPosition;

    private Quaternion _baseLocalRotation;
}
