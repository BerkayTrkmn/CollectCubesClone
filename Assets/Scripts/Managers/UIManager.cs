using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [Header("Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject successPanel;
    [SerializeField] private GameObject failPanel;

    [SerializeField] private GameObject nextLevelButton;

    [Header("TextMeshPro")]
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private List<Vacuum> vacuumList;
    private void OnEnable()
    {
        Config.OnGameEnd?.AddListener(OnGameEnd);
        Config.OnGameStart?.AddListener(OnGameStart);
        Config.OnGameSuccess?.AddListener(OnGameSuccess);
        Config.OnGameFailed?.AddListener(OnGameFailed);
    }

    private void OnDisable()
    {
        Config.OnGameEnd?.RemoveListener(OnGameEnd);
        Config.OnGameStart?.RemoveListener(OnGameStart);
        Config.OnGameSuccess?.RemoveListener(OnGameSuccess);
        Config.OnGameFailed?.RemoveListener(OnGameFailed);
    }
    #region Events
    private void OnGameEnd()
    {
        //player won
        if (CheckWinner().ID == 0)
            Config.OnGameSuccess?.Invoke();
        else
            Config.OnGameFailed?.Invoke();
    }
    private void OnGameStart()
    {
        startPanel.SetActive(false);
        Config.LevelState = LevelState.Play;
    }
    private void OnGameSuccess()
    {
        successPanel.SetActive(true);
        Config.LevelState = LevelState.Completed;
    }
    private void OnGameFailed()
    {
        failPanel.SetActive(true);
        Config.LevelState = LevelState.Failed;
    }
    #endregion

    public void GameStart()
    {
        Config.OnGameStart?.Invoke();
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void NextLevel()
    {
        //Oyunda level olmadýðý için tekrar baþlatýyorum ama level de konulabilir
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private Character CheckWinner()
    {
        int _bestNumber = 0;
        Character _bestCharacter = null;
        foreach (Vacuum vacuum in vacuumList)
        {
            if (vacuum.UsedPlayer.collectedCubes > _bestNumber)
            {
                _bestCharacter = vacuum.UsedPlayer;
                _bestNumber = vacuum.UsedPlayer.collectedCubes;
            }

        }
        return _bestCharacter;
    }

}