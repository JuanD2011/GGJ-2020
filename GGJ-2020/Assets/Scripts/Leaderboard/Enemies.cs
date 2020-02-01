using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    string[] _enemies = { "grandma", "tricycle", "Tues", "Wed", "Thurs", "Fri", "Sat" };

    private int GetEnemy(string enemy)
    {

        for (int i = 0; i < _enemies.Length; i++)
            if (_enemies[i] == enemy)
                return i;

        throw new System.ArgumentOutOfRangeException();
    }

    public int this[string enemy]
    {
        get
        {
            return (GetEnemy(enemy));
        }
    }
}

public class Enemy
{
    float enemyProb;
}


