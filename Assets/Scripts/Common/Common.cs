using System;
using UnityEngine;
using static System.Array;
using static UnityEngine.Quaternion;
using static UnityEngine.Time;
using static UnityEngine.Vector3;

public static class Common
{
    public const string AXIS_HORIZONTAL = "Horizontal";

    public static bool IsIn<TEnum>(TEnum e, params TEnum[] set) where TEnum : Enum => IndexOf(set, e) >= 0;

    public static bool TickDown(ref float t)
    {
        if (t <= 0)
        {
            return false;
        }

        t -= deltaTime;

        return t > 0;
    }

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
