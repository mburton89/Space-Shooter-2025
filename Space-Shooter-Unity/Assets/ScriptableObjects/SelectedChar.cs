using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SelectedChar : ScriptableObject
{
    public CharacterSO selectedCharacter;

    public void SetCharacter(CharacterSO character)
    {
        selectedCharacter = character;
    }
}
