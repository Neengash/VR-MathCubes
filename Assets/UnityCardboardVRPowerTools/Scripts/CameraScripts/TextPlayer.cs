using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPlayer : MonoBehaviour
{
    public static TextPlayer instance;

    [SerializeField] public TextMeshProUGUI text;
    [SerializeField] float timeBetweenTypes = 0.02f;
    [SerializeField] float timeUntilClear = 5f;

    Coroutine currentCorroutine = null;

    private void Start() {
        instance = this;
    }
    public void writeSentence(string sentence) {
        if (currentCorroutine != null) { StopCoroutine(currentCorroutine); }
        currentCorroutine = StartCoroutine(type(sentence));
    }

    IEnumerator type(string sentence) {
        
        text.text = "";

        foreach (char letter in sentence.ToCharArray()) {
            text.text += letter;
            yield return new WaitForSeconds(timeBetweenTypes);
        }

        yield return new WaitForSeconds(timeUntilClear);

        text.text = "";
        currentCorroutine = null;
    }
}
