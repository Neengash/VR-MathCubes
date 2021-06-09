using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/ScriptableLevel", order = 1)]
public class ScriptableLevel : ScriptableObject
{
    public int proportionRed;
    public int proportionGrey;
    public string objectiveText;
}
