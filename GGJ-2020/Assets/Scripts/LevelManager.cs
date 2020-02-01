using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    private AsyncOperation operation = null;

    private MenuManager menuManager = null;

    protected virtual void Awake()
    {
        menuManager = GetComponent<MenuManager>();
    }

    protected virtual void Start()
    {
         menuManager.OnLoadLevel += LoadLevel;
    }

    private void LoadLevel(string _levelBuildId)
    {
        StartCoroutine(LoadAsynchronously(_levelBuildId));
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