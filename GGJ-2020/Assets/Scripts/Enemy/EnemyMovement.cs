using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    float direction = -1;
    float speed = 1;    

    public OwnEventSystem.GameEvent OnLimit;

    private void Start()
    {
        direction = transform.position.x > 0 ? -1 : 1;
        if (direction == 1) transform.eulerAngles = new Vector3(0, 90, 0);
        else transform.eulerAngles = new Vector3(0, -90, 0);
        speed = EnemyController.CalculateVelocity(speed, 1);
    }

    public void Movement()
    {
        transform.position += Vector3.right * direction * speed * Time.deltaTime;
        //transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
        if (CheckLimits.OnVisible(transform.position))
        {
            OnLimit.Raise();
        }
    }
}
