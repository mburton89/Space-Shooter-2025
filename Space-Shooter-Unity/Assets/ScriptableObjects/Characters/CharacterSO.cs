using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class CharacterSO : ScriptableObject
{
    [Header("Character Information")]
    public Sprite portrait;
    public string _name;
    public string bio;

    [Header("Stats")]
    public int health;
    public float acceleration;
    public float movementSpeed;
    public float projectileSpeed;
    public float fireRate;

    [Header("Ship Prefab")]
    public GameObject characterShip;
}
