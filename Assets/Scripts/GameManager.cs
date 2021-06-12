using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] VRPointer vrCamera;
    [SerializeField] GameObject mainMenu, pauseMenu, successMenu;
    [SerializeField] LevelController[] levels;
    [SerializeField] Button nextLevelButton;

    TimerController timerController;
    LevelController currentLevel;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        timerController = FindObjectOfType<TimerController>();
    }

    public void PauseGame() {
        timerController.PauseTime();
        pauseMenu.SetActive(true);
        vrCamera.ActiveVRInteractions = false;
    }

    public void ResumeGame() {
        timerController.ResumeTime();
        pauseMenu.SetActive(false);
        vrCamera.ActiveVRInteractions = true;
    }

    public int GetLevelsCount() {
        return levels.Length;
    }

    public bool CheckWinCondition() {
        return currentLevel.CheckWinCondition();
    }

    public void LevelComplete() {
        timerController.PauseTime();
        CheckNextLevelButton();
        successMenu.SetActive(true);
        vrCamera.ActiveVRInteractions = false;
        if (LevelSelectController.instance.currentLevel == LevelSelectController.instance.maxLevel) {
            LevelSelectController.instance.maxLevel++;
        }
        LevelSelectController.instance.UpdateInterface();
    }

    private void CheckNextLevelButton() {
        if (LevelSelectController.instance.currentLevel == levels.Length -1) {
            DissableButton();
        } else {
            EnableButton();
        }
    }

    private void EnableButton() {
        ColorBlock colors = nextLevelButton.colors;
        colors.colorMultiplier = 5f;
        nextLevelButton.colors = colors;
        nextLevelButton.GetComponent<VRInteraction>().InteractionsEnabled = true; 
    }

    private void DissableButton() {
        ColorBlock colors = nextLevelButton.colors;
        colors.colorMultiplier = 1f;
        nextLevelButton.colors = colors;
        nextLevelButton.GetComponent<VRInteraction>().InteractionsEnabled = false; 
    }

    public void AdvanceLevel() {
        if (currentLevel != null) {
            Destroy(currentLevel.gameObject);
        }
        LevelSelectController.instance.IncreaseLevel();
        LoadLevel();
    }

    public void ReturnMainMenu() {
        if (currentLevel != null) {
            Destroy(currentLevel.gameObject);
        }
        timerController.PauseTime();
        timerController.ResetTime();
        successMenu.SetActive(false);
        pauseMenu.SetActive(false);
        mainMenu.SetActive(true);
        vrCamera.ActiveVRInteractions = false;
    }

    public void LoadLevel() {
        timerController.ResetTime();
        timerController.ResumeTime();
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        successMenu.SetActive(false);
        vrCamera.ActiveVRInteractions = true;
        currentLevel = Instantiate(levels[LevelSelectController.instance.currentLevel]);
        TextPlayer.instance.writeSentence(currentLevel.getObjectiveSentence());
    }
}
