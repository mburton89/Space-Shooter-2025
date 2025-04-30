using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharSelectUI : MonoBehaviour
{
    public Image sprite;
    public TextMeshProUGUI bio;
    public TextMeshProUGUI l_name;

    public CurrentCharacterInfo currentCharacter;


    private void Start()
    {
        currentCharacter = GetComponent<CurrentCharacterInfo>();
    }
    public void UpdateUi()
    {
        sprite.sprite = currentCharacter.curChar.portrait;
        bio.text = currentCharacter.curChar.bio;
        l_name.text = currentCharacter.curChar._name;
    }

}
