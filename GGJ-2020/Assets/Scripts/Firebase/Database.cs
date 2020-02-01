using UnityEngine;
using Firebase.Database;
using Firebase;
using Firebase.Unity.Editor;

public class Database : MonoBehaviour
{
    public static Database DB = null;
    public DatabaseReference dbRef;
    public FirebaseApp app;

    private void Awake()
    {
        if (DB == null) DB = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://ggj-2020.firebaseio.com/");
    }

    public void WriteUser(UserFB user)
    {
        Debug.Log("Writing new user in database bitch...");
        string userJson = JsonUtility.ToJson(user);
        dbRef.Child("users").Child(user.userID).SetRawJsonValueAsync(userJson);
    }

    public void WriteNewLvl(int lvl)
    {
        dbRef.Child("users").Child(Authentication.myUser.UserId).Child("currentLevel").SetValueAsync(lvl).ContinueWith(task =>
        {
            if(task.IsCompleted)
            {
                Debug.Log("Write lvl succesfull");
            }

        });

    }

}
