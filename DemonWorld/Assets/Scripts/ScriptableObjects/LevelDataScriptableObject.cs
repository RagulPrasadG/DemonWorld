using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMapData" , menuName = "Data/NewMapData")]
public class LevelDataScriptableObject : ScriptableObject
{
    public int levelWidth;
    public int levelHeight;
    public float cellSize;
    public int waves;
    public GameObject pathTile;
    public GameObject emptyTile;
    public GameObject wallPrefab;
    public GameObject cornerWallPrefab;
    public TextAsset csvFile;
    [SerializeField] List<Vector3> wayPointsIndex;
    public GameObject startTile;
    public GameObject endTile;
    public GameObject gate;

    public List<Vector3> GetWayPoints() => wayPointsIndex;

}
