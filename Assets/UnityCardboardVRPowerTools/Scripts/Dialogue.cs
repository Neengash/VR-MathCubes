using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{

    [SerializeField, TextArea(1, 3)]
    string sentence;

    public void writeSentence() {
        TextPlayer.instance.writeSentence(sentence);
    }
}
