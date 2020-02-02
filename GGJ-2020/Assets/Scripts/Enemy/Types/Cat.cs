using System.Collections;
using UnityEngine;

public class Cat : EnemyMovement
{
    float jumpForce = 40f;
    float jumpFrequency = 2.0f;
    [SerializeField] AnimationCurve animationCurve;

    WaitForSeconds waitForJump;
    private void Start()
    {
        waitForJump = new WaitForSeconds(jumpFrequency);
        StartCoroutine(Jump());
    }
    public IEnumerator Jump()
    {
        float to = transform.position.y;

        yield return waitForJump;
        for (float i = 0; i < 1f; i += Time.deltaTime)
        {
            transform.position = new Vector3(transform.position.x, animationCurve.Evaluate(i) * to, transform.position.z);
            yield return null;
        }
        StartCoroutine(Jump());
    }

}

