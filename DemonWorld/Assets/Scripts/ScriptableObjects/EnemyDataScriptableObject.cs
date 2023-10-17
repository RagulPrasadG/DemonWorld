using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData" , menuName = "Data/NewEnemyData")]
public class EnemyDataScriptableObject : ScriptableObject
{
    public EnemyView enemyView;
    public EnemyType enemyType;
    public int maxHealth;
    public float speed;
    public int damageAmount;
    public int turnSpeed;
}
public enum EnemyType
{
    StoneGolem, GreenGolem
}