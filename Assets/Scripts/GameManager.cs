using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private string _playerName;
    public string PlayerName { get => _playerName; private set => _playerName = value; }

    private int _playerScore;
    public int PlayerScore { get => _playerScore; private set => _playerScore = value; }

    //for score screen

    private string _firstPlaceName;
    public string FirstPlaceName { get => _firstPlaceName; private set => _firstPlaceName = value; }

    private int _firstPlaceScore;
    public int FirstPlaceScore { get => _firstPlaceScore; private set => _firstPlaceScore = value; }

    private string _secondPlaceName;
    public string SecondPlaceName { get => _secondPlaceName; private set => _secondPlaceName = value; }

    private int _secondPlaceScore;
    public int SecondPlaceScore { get => _secondPlaceScore; private set => _secondPlaceScore = value; }

    private string _thirdPlaceName;
    public string ThirdPlaceName { get => _thirdPlaceName; private set => _thirdPlaceName = value; }

    private int _thirdPlaceScore;
    public int ThirdPlaceScore { get => _thirdPlaceScore; private set => _thirdPlaceScore = value; }





    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        ResetPlayerScore();

        LoadSavedScoreData();

        GlobalEventManager.OnGameOver += AddDataToScoreRating;
        GlobalEventManager.OnGameOver += SaveScores;

    }

    private void OnDestroy()
    {
        GlobalEventManager.OnGameOver -= AddDataToScoreRating;
        GlobalEventManager.OnGameOver -= SaveScores;
    }

    public void SetPlayerName(string enteredName)
    {
        PlayerName = enteredName;
    }

    public void IncreasePlayerScore(int point)
    {
        PlayerScore += point;

    }

    public void ResetPlayerScore()
    {
        PlayerScore = 0;
    }

    public void AddDataToScoreRating()
    {
        if (PlayerScore > FirstPlaceScore)
        {
            _thirdPlaceName = _secondPlaceName;
            ThirdPlaceScore = _secondPlaceScore;

            _secondPlaceName = _firstPlaceName;
            _secondPlaceScore = _firstPlaceScore;

            _firstPlaceName = PlayerName;
            _firstPlaceScore = PlayerScore;
        }
        else if (PlayerScore > _secondPlaceScore)
        {
            _thirdPlaceName = _secondPlaceName;
            ThirdPlaceScore = _secondPlaceScore;

            _secondPlaceName = PlayerName;
            _secondPlaceScore = PlayerScore;
        }
        else if (PlayerScore > _thirdPlaceScore)
        {
            _thirdPlaceName = PlayerName;
            ThirdPlaceScore = PlayerScore;
        }

        // Debug.Log($"First  player Name {_firstPlaceName}, Score: {_firstPlaceScore}");
        // Debug.Log($"Second  player Name {_secondPlaceName}, Score: {_secondPlaceScore}");
        // Debug.Log($"third  player Name {_thirdPlaceName}, Score: {_thirdPlaceScore}");

    }

    [System.Serializable]
    private class SaveScoreData
    {
        public string firstPlaceNameData;
        public int firstPlaceScoreData;

        public string secondPlaceNameData;
        public int secondPlaceScoreData;

        public string thirdPlaceNameData;
        public int thirdPlaceScoreData;
    }

    public void SaveScores()
    {
        SaveScoreData data = new SaveScoreData();

        data.firstPlaceNameData = FirstPlaceName;
        data.firstPlaceScoreData = FirstPlaceScore;

        data.secondPlaceNameData = SecondPlaceName;
        data.secondPlaceScoreData = SecondPlaceScore;

        data.thirdPlaceNameData = ThirdPlaceName;
        data.thirdPlaceScoreData = ThirdPlaceScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    //[ContextMenu("Read From saved Jason file")]
    public void LoadSavedScoreData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string savedJson = File.ReadAllText(path);
            Debug.Log($"Saved Jason file : {savedJson}");
            SaveScoreData savedData = JsonUtility.FromJson<SaveScoreData>(savedJson);

            FirstPlaceName = savedData.firstPlaceNameData;
            FirstPlaceScore =savedData.firstPlaceScoreData;

            SecondPlaceName = savedData.secondPlaceNameData;
            SecondPlaceScore = savedData.secondPlaceScoreData;

            ThirdPlaceName = savedData.thirdPlaceNameData;
            ThirdPlaceScore = savedData.thirdPlaceScoreData;
        }
    }
}
