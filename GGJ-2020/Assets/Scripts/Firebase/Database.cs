using UnityEngine;
using Firebase.Database;
using Firebase;
using Firebase.Unity.Editor;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

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

    public async Task<List<Dictionary<string,string>>> GetUserList()
    {
        string json = string.Empty;

        List<Dictionary<string, string>> userList = new List<Dictionary<string, string>>();
        int index = 0;

        await dbRef.Child("users").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach(DataSnapshot data in snapshot.Children)
                {                    
                    json = data.GetRawJsonValue();
                    Dictionary<string,string> values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    userList.Add(values);
                    Debug.Log(json);
                    Debug.LogFormat("Current level of {0} is: {1}", userList[index]["name"], userList[index]["currentLevel"]);
                    index++;

                }                      
                Debug.Log("Get Player Data As JSON Succesful");                
            }
        });

        return userList;

    }

}
