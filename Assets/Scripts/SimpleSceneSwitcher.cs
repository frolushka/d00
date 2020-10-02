using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneSwitcher : MonoBehaviour
{
    [SerializeField] private List<string> scenes;

    private int _currentSceneIndex;
    
    private void Awake()
    {
        SceneManager.LoadScene(scenes[_currentSceneIndex], LoadSceneMode.Additive);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.UnloadScene(scenes[_currentSceneIndex]);
            _currentSceneIndex--;
            if (_currentSceneIndex < 0)
                _currentSceneIndex = scenes.Count - 1;
            SceneManager.LoadScene(scenes[_currentSceneIndex], LoadSceneMode.Additive);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.UnloadScene(scenes[_currentSceneIndex]);
            _currentSceneIndex++;
            if (_currentSceneIndex >= scenes.Count)
                _currentSceneIndex = 0;
            SceneManager.LoadScene(scenes[_currentSceneIndex], LoadSceneMode.Additive);
        }
    }
}
