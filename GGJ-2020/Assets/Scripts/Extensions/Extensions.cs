using UnityEngine;

public static class Extensions
{
    public static Vector3 GetRandomPointInVolume(this Collider collider)
    {
        Vector3 result = Vector3.zero;
        result = new Vector3(Random.Range(collider.bounds.min.x, collider.bounds.max.x), collider.transform.position.y, Random.Range(collider.bounds.min.z, collider.bounds.max.z));
        return result;
    }
}
