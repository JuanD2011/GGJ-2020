using Firebase.Auth;
using System.Collections;
using UnityEngine;

public class Authentication : MonoBehaviour
{
    FirebaseAuth auth;
    public static FirebaseUser myUser;

    private IEnumerator Start()
    {
        yield return FirebaseDependencies.waitUntilCheckDependencies;
        auth = FirebaseAuth.DefaultInstance;
        AnonymousAuth();
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if(auth.CurrentUser != myUser)
        {
            bool signedIn = myUser != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && myUser != null)
                Debug.Log("Signed out " + myUser.UserId);

            myUser = auth.CurrentUser;

            if(signedIn)
                Debug.Log("Signed in " + myUser.UserId);

        }
    }

    void AnonymousAuth()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => {

            if (task.IsCanceled)
                Debug.LogFormat("Task was canceled");
            if (task.IsFaulted)
                Debug.LogFormat("Task was faulted: {0}", task.Exception);

            myUser = task.Result;
            UserFB userFB = new UserFB { currentLevel = "0", name = "VIVAELPERICO", userID = myUser.UserId };

            Database.DB.WriteUser(userFB);
            Debug.LogFormat("User signed in successfully: {0} ({1})", myUser.DisplayName, myUser.UserId);
        });
    }

}
