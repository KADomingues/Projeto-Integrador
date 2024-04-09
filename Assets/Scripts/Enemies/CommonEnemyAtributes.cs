using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scriptable Objecties/New Enemy")]
public class CommonEnemyAtributes : ScriptableObject
{
    public int HP;
    public int experienceOnDeath;
    public float moveSpeed;
    public float playerDamage;
    public float playerScore;
    public GameObject[] commonDropTable;
}