using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data",menuName = "Scriptable/unallowedLetters")]
public class UnallowedLetterScriptable : ScriptableObject
{
    public List<char> unallowedLetters;
}
