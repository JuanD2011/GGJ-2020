using Firebase;
using System;
using UnityEngine;

public class FirebaseDependencies : MonoBehaviour
{
    public static System.Func<bool> CheckDependenciesHandler = () => {
        return Firebase.FirebaseApp.CheckDependencies() == Firebase.DependencyStatus.Available;
    };

    public static WaitUntil waitUntilCheckDependencies =  new WaitUntil(() => CheckDependenciesHandler.Invoke());

    public FirebaseApp app;

    private void Start()
    {
        InitializeDB();
    }

    private void InitializeDB()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            DependencyStatus dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
                app = FirebaseApp.DefaultInstance;
            else
                Debug.LogError(string.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        });
    }
}
