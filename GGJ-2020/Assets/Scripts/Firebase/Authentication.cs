using Firebase.Auth;
using System.Collections;
using UnityEngine;

public class Authentication : MonoBehaviour
{
    public static Authentication instance = null;

    private FirebaseAuth auth;
    public static FirebaseUser myUser;

    public event Delegates.Action OnLoggedIn = null;
    public event Delegates.Action OnSignedOut = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Start()
    {
        yield return FirebaseDependencies.waitUntilCheckDependencies;
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if(auth.CurrentUser != myUser)
        {
            bool signedIn = myUser != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && myUser != null)
            {
                OnSignedOut?.Invoke();
                Debug.Log("Signed out " + myUser.UserId);
            }

            myUser = auth.CurrentUser;

            if (signedIn)
            {
                Debug.Log("Signed in " + myUser.UserId);
                OnLoggedIn?.Invoke();
            }
        }
    }

    public void AnonymousAuth(string _username)
    {
        auth.SignInAnonymouslyAsync().ContinueWith(task => {

            if (task.IsCanceled)
                Debug.LogFormat("Task was canceled");
            if (task.IsFaulted)
                Debug.LogFormat("Task was faulted: {0}", task.Exception);

            myUser = task.Result;
            UserFB userFB = new UserFB { currentLevel = "0", name = _username, userID = myUser.UserId, hasSetUsername = true };

            Database.DB.WriteUser(userFB);
            Debug.LogFormat("User signed in successfully: {0} ({1})", myUser.DisplayName, myUser.UserId);
        });
    }

    public void SignOut()
    {
        auth.SignOut();
    }
}
