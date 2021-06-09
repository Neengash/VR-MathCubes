using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] ScriptableLevel levelConfig;

    BoxController[] levelCubes;

    void Start() {
        PopulateLevelCubes();
        do {
            SetRandomCubesMaterials();
        } while (CheckWinCondition());
    }

    public bool CheckWinCondition() {
        int counterRed = 0, counterGrey = 0;
        foreach (BoxController box in levelCubes) {
            if (box.currentMaterial == 0) {
                counterRed++;
            } else {
                counterGrey++;
            }
        }

        return counterRed / levelConfig.proportionRed == counterGrey / levelConfig.proportionGrey;
    }

    public string getObjectiveSentence() {
        return levelConfig.objectiveText;
    }

    private void PopulateLevelCubes() {
        int i = 0;
        levelCubes = new BoxController[transform.childCount];
        foreach (Transform child in transform) {
            levelCubes[i++] = child.gameObject.GetComponent<BoxController>();
        }
    }

    private void SetRandomCubesMaterials() {
        foreach (BoxController box in levelCubes) {
            box.SetRandomMaterial();
        }
    }

}
