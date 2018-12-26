using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour, IUIManager {

    [Header("UI Objects")]
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private Text gameTypeButtonText;
    [SerializeField]
    private Button loadButton;
    [SerializeField]
    private Button saveButton;
    [SerializeField]
    private Text p1ScoreText;
    [SerializeField]
    private Text p2ScoreText;
    [SerializeField]
    private Text bigText;


    private bool menuDisplayed;

    private IGameStateManager manager;
    private Settings settings;

    public void Init(IGameStateManager gm, Settings newSettings) {
        manager = gm;
        settings = newSettings;
    }


    private void Start() {
        ShowMenu();
    }

    public void ChangeGameType() {
        manager.player2Human = !manager.player2Human;
        gameTypeButtonText.text = manager.player2Human ? "2 Players" : "1 Player";
    }

    public void PlayClicked() {
        HideMenu();
        manager.StartGame(true, manager.player2Human);
    }

    public void ShowMenu() {

        if (manager.HasSavedGame()){
            loadButton.interactable = true;
        } else {
            loadButton.interactable = false;
        }

        if (manager.playerGameOn) {
            saveButton.interactable = true;
        } else {
            saveButton.interactable = false;
        }

        menu.transform.localPosition = Vector3.zero;
        menuDisplayed = true;
    }

    public void HideMenu() {
        menu.transform.localPosition = Vector3.right * 1500;
        menuDisplayed = false;
    }

    public void QuitClicked() {
        Application.Quit();
    }

    private void Update() {
        if (Input.GetKeyUp(settings.pauseKey)) {
            if (manager.playerGameOn) {
                if (menuDisplayed) {
                    manager.SetPaused(false);
                    HideMenu();
                } else {
                    manager.SetPaused(true);
                    ShowMenu();
                }
            }
        }
    }

    public void UpdatePoints(int p1Points, int p2Points) {
        p1ScoreText.text = p1Points.ToString();
        p2ScoreText.text = p2Points.ToString();
    }

    public void ShowText(string message) {
        bigText.text = message;
        bigText.gameObject.SetActive(true);
        Invoke("HideText", settings.messageDisplayTime);
    }

    private void HideText() {
        bigText.gameObject.SetActive(false);
    }

    public void SaveButtonClicked() {
        manager.SaveGame();
        HideMenu();
        ShowText("Game Saved");
        manager.SetPaused(false);
    }

    public void LoadButtonClicked() {
        manager.LoadGame();
        HideMenu();
        ShowText("Game Loaded");
        manager.SetPaused(false);
    }
}
