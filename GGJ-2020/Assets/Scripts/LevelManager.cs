using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance = null;

    private AsyncOperation operation = null;

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

    public void LoadLevel(string _levelBuildId)
    {
        StartCoroutine(LoadAsynchronously(_levelBuildId));
    }

    public void LoadSameLevel()
    {
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().name));
    }

    public void LoadHome()
    {
        StartCoroutine(LoadAsynchronously("Menu"));
    }

    protected IEnumerator LoadAsynchronously(string _levelBuildId)
    {
        Scene scene = SceneManager.GetSceneByName(_levelBuildId);

        if (scene != null)
        {
            operation = SceneManager.LoadSceneAsync(_levelBuildId);

        }
        else
        {
            operation = SceneManager.LoadSceneAsync(0);//If the level that is trying to load does not exist, then load menu scene
        }

        float progress = 0f;
        
        while (!operation.isDone)
        {
            progress = Mathf.Clamp01(operation.progress * 0.9f);
            yield return null;
        }
    }
}