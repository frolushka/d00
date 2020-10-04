using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneSwitcher : MonoBehaviour
{
    [SerializeField] private List<string> scenes;

    private int _currentSceneIndex;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(scenes[_currentSceneIndex]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            _currentSceneIndex--;
            if (_currentSceneIndex < 0)
                _currentSceneIndex = scenes.Count - 1;
            SceneManager.LoadScene(scenes[_currentSceneIndex]);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            _currentSceneIndex++;
            if (_currentSceneIndex >= scenes.Count)
                _currentSceneIndex = 0;
            SceneManager.LoadScene(scenes[_currentSceneIndex]);
        }
    }
}
