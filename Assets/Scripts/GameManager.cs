using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IGameSaver))]
public class GameManager : MonoBehaviour, IGameStateManager, IGameScoreManager {

    public Settings settings;
    private IGameSaver saver;
    private IUIManager uiManager;
    
    [Header("Game Objects")]
    [SerializeField]
    private GameObject uiObject;
    [SerializeField]
    private GameObject p1Paddle;
    [SerializeField]
    private GameObject p2Paddle;
    [SerializeField]
    private BallController ball;

    public bool playerGameOn { get; private set; }
    public bool player2Human { get; set; }

    private Vector3 p1StartPos;
    private Vector3 p2StartPos;

    private int[] playerPoints = new int[2];

    void Start() {

        saver = GetComponent<IGameSaver>();
        uiManager = uiObject.GetComponent<IUIManager>();

        player2Human = false;

        p1StartPos = new Vector3(settings.paddlePositionX, 0, 0);
        p2StartPos = new Vector3(-settings.paddlePositionX, 0, 0);

        uiManager.Init(this, settings);
        ball.Init(this, settings);
        p1Paddle.GetComponent<PaddleMotor>().Init(settings);
        p2Paddle.GetComponent<PaddleMotor>().Init(settings);
        StartGame(false, false);

    }

    public void StartGame(bool p1Human, bool p2Human, int p1points = 0, int p2points = 0) {

        // Init player data
        playerGameOn = p1Human;
        playerPoints[0] = p1points;
        playerPoints[1] = p2points;
        UpdateUI();

        // Init paddles
        ResetPaddlePositions();
        SetPaddleInput(p1Paddle, p1Human, 1);
        SetPaddleInput(p2Paddle, p2Human, 2);

        // Start game
        ball.StartNewPoint(playerGameOn);
        SetPaused(false);
    }

    private void ResetPaddlePositions() {
        p1Paddle.transform.position = p1StartPos;
        p2Paddle.transform.position = p2StartPos;
    }

    void SetPaddleInput(GameObject paddleGO, bool isHuman, int index) {

        if (paddleGO.GetComponent<IPaddleInput>() != null) {
            paddleGO.GetComponent<IPaddleInput>().Destroy();
        }

        if (isHuman) {
            Debug.Log("Setting Human Input");
            PlayerInput playerInput = paddleGO.AddComponent<PlayerInput>();
            if (index == 1) {
                playerInput.SetInputKeys(settings.player1UpKey, settings.player1DownKey);
            } else {
                playerInput.SetInputKeys(settings.player2UpKey, settings.player2DownKey);
            }
        } else {
            Debug.Log("Setting AI Input");
            AIInput aiInput = paddleGO.AddComponent<AIInput>();
            aiInput.SetTarget(ball.transform);
        }
        paddleGO.GetComponent<PaddleMotor>().InitInput();
    }

    public void AddPoints(int player, int points) {
        
        if (playerGameOn) {
            playerPoints[player-1]++;
            UpdateUI();
            if (playerPoints[player-1] >= settings.pointsToWin) {
                playerGameOn = false;
                ShowWinText();
            } else {
                ball.StartNewPoint(playerGameOn);
            }
        } else {
            ball.StartNewPoint(playerGameOn);
        }
    }

    private void UpdateUI() {
        uiManager.UpdatePoints(playerPoints[0], playerPoints[1]);
    }

    private void ShowWinText() {
        string winner;
        if (player2Human) {
            winner = (playerPoints[0] == settings.pointsToWin) ? "Player 1" : "Player 2";
        } else {
            winner = (playerPoints[0] == settings.pointsToWin) ? "Player" : "Computer";
        }
        uiManager.ShowText(winner + " Wins!!!"); 
        Invoke("SwitchToMenu", settings.messageDisplayTime);
    }

    private void SwitchToMenu() {
        uiManager.ShowMenu();
        StartGame(false, false);
    }

    #region Serialization
    public void LoadGame() {
        SaveData data;
        if (saver.LoadGame(out data)) {
            player2Human = data.player2Human;
            StartGame(true, data.player2Human, data.player1Points, data.player2Points);
        } else {
            uiManager.ShowText("Error loading data");
        }
    }

    public void SaveGame() {
        SaveData data;
        data.player1Points = playerPoints[0];
        data.player2Points = playerPoints[1];
        data.player2Human = player2Human;
        saver.SaveGame(data);
        //ball.StartNewPoint(true);
    }

    public bool HasSavedGame() {
        return saver.HasSavedGame();
    }

    public void SetPaused(bool paused) {
        Time.timeScale = paused ? 0 : 1;
    }
    #endregion
}
