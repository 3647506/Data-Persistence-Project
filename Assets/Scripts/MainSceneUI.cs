using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainSceneUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _playerScore;

    [SerializeField]
    private GameObject _scoreTableGameObject;

    [SerializeField]
    private TMP_Text _firstPlaceScoreText;

    [SerializeField]
    private TMP_Text _secondPlaceScoreText;

    [SerializeField]
    private TMP_Text _thirdPlaceScoreText;

    [SerializeField]
    private TMP_Text _currentScoreSituation;

    [SerializeField]
    private TMP_Text _bestScoreOnTopScreen;

    private void Awake()
    {
        GlobalEventManager.OnBlockDestroed += SetPlayerScoreText;
        GlobalEventManager.OnGameOver += ShowScoreTable;

        if (GameManager.Instance != null)
        {
            SetPlayerScoreText();
        }

        _scoreTableGameObject.SetActive(false);

        if (GameManager.Instance.FirstPlaceName != null && GameManager.Instance.FirstPlaceName != "")
        {
            _bestScoreOnTopScreen.text = "Best Score : " + GameManager.Instance.FirstPlaceName + " :" + GameManager.Instance.FirstPlaceScore;
        }
    }

    private void OnDestroy()
    {
        GlobalEventManager.OnBlockDestroed -= SetPlayerScoreText;
        GlobalEventManager.OnGameOver -= ShowScoreTable;
    }

    private void SetPlayerScoreText()
    {
        _playerScore.text = GameManager.Instance.PlayerName + " score:" + GameManager.Instance.PlayerScore;
    }

    public void ShowScoreTable()
    {
        _scoreTableGameObject.SetActive(true);

        if (GameManager.Instance.FirstPlaceName != null && GameManager.Instance.FirstPlaceName != "")
        {
            _firstPlaceScoreText.gameObject.SetActive(true);
            _firstPlaceScoreText.text = GameManager.Instance.FirstPlaceName + " : " + GameManager.Instance.FirstPlaceScore;
        }

        if (GameManager.Instance.SecondPlaceName != null && GameManager.Instance.SecondPlaceName != "")
        {
            _secondPlaceScoreText.gameObject.SetActive(true);
            _secondPlaceScoreText.text = GameManager.Instance.SecondPlaceName + " : " + GameManager.Instance.SecondPlaceScore;
        }

        if (GameManager.Instance.ThirdPlaceName != null && GameManager.Instance.ThirdPlaceName != "")
        {
            _thirdPlaceScoreText.gameObject.SetActive(true);
            _thirdPlaceScoreText.text = GameManager.Instance.ThirdPlaceName + " : " + GameManager.Instance.ThirdPlaceScore;
        }

        _currentScoreSituation.text = "Your score: " + GameManager.Instance.PlayerScore;

    }


}
