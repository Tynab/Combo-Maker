using UnityEngine;
using static UnityEngine.Quaternion;
using static UnityEngine.Vector3;

public static class Common
{
    public const string AXIS_HORIZONTAL = "Horizontal";

    public static void Active(this GameObject gameObject)
    {
        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }

    public static void Deactive(this GameObject gameObject)
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    public static void ResetPositionAndRotation(this GameObject gameObject) => gameObject.transform.SetLocalPositionAndRotation(zero, identity);

    public static void ResetPositionAndRotation(this GameObject gameObject, Vector3 position) => gameObject.transform.SetLocalPositionAndRotation(position, identity);
}
