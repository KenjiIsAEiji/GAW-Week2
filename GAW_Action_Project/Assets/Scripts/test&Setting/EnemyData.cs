using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "MyScriptableObjct/Create EnemyData")]
public class EnemyData : ScriptableObject
{
    public float EnemyMaxSpeed = 2f;
    public float MaxHp = 10f;
    public float AttackPoint = 10f;
}