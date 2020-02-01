using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseDependencies : MonoBehaviour
{
    public static System.Func<bool> CheckDependenciesHandler = () => {
        return Firebase.FirebaseApp.CheckDependencies() == Firebase.DependencyStatus.Available;
    };

    public static WaitUntil waitUntilCheckDependencies =  new WaitUntil(() => CheckDependenciesHandler.Invoke());

}
