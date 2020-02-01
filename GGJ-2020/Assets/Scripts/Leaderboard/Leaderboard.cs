using UnityEngine;
using Firebase.Database;

public class Leaderboard : MonoBehaviour
{
    FirebaseDatabase DB;
    private void Start()
    {
        DB = FirebaseDatabase.DefaultInstance;
    }
}
