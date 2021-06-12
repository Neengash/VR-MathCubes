using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelectController : MonoBehaviour
{
    public static LevelSelectController instance;

    [SerializeField] Button lvlUp, lvlDown;
    [SerializeField] TextMeshProUGUI levelText;
    public int currentLevel { get; private set;}
    public int maxLevel { get; set; }

    const int MIN_LEVEL = 0;
    const float BUTTON_DISSABLED = 1f;
    const float BUTTON_ENABLED = 5f;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        currentLevel = 0;
        maxLevel = 0;
        CheckLevelButtonsAvailable();
    }

    public void IncreaseLevel() {
        currentLevel++;
        UpdateLevelText();
        CheckLevelButtonsAvailable();
    }

    public void UpdateInterface() {
        UpdateLevelText();
        CheckLevelButtonsAvailable();
    }

    public void DecreaseLevel() {
        currentLevel--;
        UpdateLevelText();
        CheckLevelButtonsAvailable();
    }

    private void UpdateLevelText() {
        string level = currentLevel.ToString();
        if (currentLevel < 10) { level = "0" + level; }
        levelText.text = level;
    }

    private void CheckLevelButtonsAvailable() {
        if (
            currentLevel < maxLevel &&
            currentLevel < GameManager.instance.GetLevelsCount() -1
        ) {
            EnableButton(lvlUp);
        } else {
            DissableButton(lvlUp);
        }

        if (currentLevel > MIN_LEVEL) {
            EnableButton(lvlDown);
        } else {
            DissableButton(lvlDown);
        }
    }

    private void EnableButton(Button button) {
        SetColorMultiplier(button, BUTTON_ENABLED);
        SetButtonVRInteraction(button, true);
    }

    private void DissableButton(Button button) {
        SetColorMultiplier(button, BUTTON_DISSABLED);
        SetButtonVRInteraction(button, false);
    }

    private void SetColorMultiplier(Button button, float multiplier) {
        ColorBlock colors = button.colors;
        colors.colorMultiplier = multiplier;
        button.colors = colors;
    }

    private void SetButtonVRInteraction(Button button, bool value) {
        button.GetComponent<VRInteraction>().InteractionsEnabled = value; 
    }
}
