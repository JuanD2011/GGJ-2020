using Firebase.Auth;
using UnityEngine;

public class Authentication : MonoBehaviour
{
    FirebaseAuth auth;
    FirebaseUser myUser;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {

    }



    void AnonymousAuth()
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => {

            if (task.IsCanceled) Debug.LogFormat("Task was canceled");
            if (task.IsFaulted) Debug.LogFormat("Task was faulted: {0}", task.Exception);

            myUser = task.Result;
            Debug.Log("Log In Succesful");
        });
    }


}
