using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float direction = -1;
    float speed = 1;    

    public OwnEventSystem.GameEvent OnLimit;

    private void Start()
    {
        direction = CheckLimits.LeftOrRight(transform.position) ? -1 : 1;
    }

    public void Movement()
    {
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
        if (CheckLimits.OnVisible(transform.position))
        {
            OnLimit.Raise();
        }
    }
}
