using UnityEngine;

public class CheckLimits : MonoBehaviour
{
    public static bool OnVisible(Vector3 pos)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(pos);
        return screenPoint.x < -2 || screenPoint.x > 2 ? false : true;
    }

    public static bool LeftOrRight(Vector3 pos)
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(pos);
        return screenPoint.x < 0 ? false : true;
    }

}
