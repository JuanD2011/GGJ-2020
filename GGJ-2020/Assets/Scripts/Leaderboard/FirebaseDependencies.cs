using UnityEngine;

public class FirebaseDependencies
{
    public static System.Func<bool> CheckDependenciesHandler = () => {
        return Firebase.FirebaseApp.CheckDependencies() == Firebase.DependencyStatus.Available;
    };

    public static WaitUntil waitUntilCheckDependencies =  new WaitUntil(() => CheckDependenciesHandler.Invoke());


}
