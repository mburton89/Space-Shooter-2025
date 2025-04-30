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
    private bool isSelected = false;


    private void Start()
    {
        currentCharacter = GetComponent<CurrentCharacterInfo>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
    }
    public void UpdateUi()
    {
        setSpriteVisibility();
        sprite.sprite = currentCharacter.curChar.portrait;
        bio.text = currentCharacter.curChar.bio;
        l_name.text = currentCharacter.curChar._name;
    }

    private void setSpriteVisibility()
    {
        if (!isSelected)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 255f);
            isSelected = true;
        }
    }   
}
