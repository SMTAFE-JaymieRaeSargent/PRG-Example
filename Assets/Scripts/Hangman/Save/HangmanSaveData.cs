using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HangmanSaveData
{
    public string currentWord;
    public List<char> lettersGuessed = new List<char>();
}
