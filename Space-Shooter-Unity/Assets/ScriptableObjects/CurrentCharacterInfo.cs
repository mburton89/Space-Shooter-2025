using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CurrentCharacterInfo : MonoBehaviour
{
    public List<CharacterSO> charactersList = new();
    public CharacterSO curChar;

    public void SelectCharacter(int i)
    {
        curChar = i > charactersList.Count ? charactersList[i] : null;
    }

    public CharacterSO GetCurChar()
    {
        return curChar;
    }
}
