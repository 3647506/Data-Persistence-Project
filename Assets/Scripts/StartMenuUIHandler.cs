using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartMenuUIHandler : MonoBehaviour
{
    // [SerializeField]
    // private TMP_InputField _inputText;

    [SerializeField]
    private TMP_Text EnteredName;

    [SerializeField]
    private string _inputName;

    public string InputName { get => _inputName; private set => _inputName = value; }

    public void RecordEnteredName(string inputText)
    {
        _inputName = inputText;
        Debug.Log($"Recorder name: {_inputName}");
        GameManager.Instance.SetPlayerName(_inputName);
        ShowEnteredName();
    }

    private void ShowEnteredName()
    {
        if (EnteredName.gameObject.activeSelf == false)
        {
            EnteredName.gameObject.SetActive(true);
        }
        EnteredName.text = "Welcome "+_inputName+"!";
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGane()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
     Application.Quit(); // original code to quit Unity player
#endif
    }
}
