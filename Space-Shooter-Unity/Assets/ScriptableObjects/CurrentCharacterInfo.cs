using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CurrentCharacterInfo : MonoBehaviour
{
    public List<CharacterSO> charactersList;
    public CharacterSO curChar;
    public SelectedChar selectedChar;
    
    public void SelectCharacter(int i)
    {
        curChar = i < charactersList.Count ? charactersList[i] : null;
        Debug.Log(curChar);
    }

    public CharacterSO GetCurChar()
    {
        return curChar;
    }

    public void SelectCharacter()
    {
        selectedChar.SetCharacter(curChar);
    }
}
