using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{    
    public static float CalculateVelocity(float baseVelocity, float currentLevel)
    {
        float velocity = 0.0f;
        float difficulty = Mathf.Exp(currentLevel) + 1;
        float randomness = Random.Range(0.15F, 0.25F);
        velocity = baseVelocity * randomness * difficulty;
        return velocity;
    }
}
