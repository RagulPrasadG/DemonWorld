using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Demonworld.Services
{
    public class LevelService
    {
        public int currentLevel = 0;
        private List<LevelDataScriptableObject> levelDataScriptableObject;
        private Cell[,] level;
        private LevelDataScriptableObject currentLevelDataScriptableObject;
        private Vector3 startPosition;

        public List<Vector3> GetCurrentLevelWayPoints() => currentLevelDataScriptableObject.GetWayPoints();
        public Vector3 GetStartPosition() => startPosition;
        public LevelService(List<LevelDataScriptableObject> levelDataScriptableObject)
        {
            this.levelDataScriptableObject = levelDataScriptableObject;
            this.currentLevelDataScriptableObject = levelDataScriptableObject[currentLevel];
            level = new Cell[currentLevelDataScriptableObject.levelWidth, currentLevelDataScriptableObject.levelHeight];
            CreateLevel();
        }

        public void CreateLevel()
        {
            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(1); j++)
                {
                    level[i, j] = new Cell(GetCellPosition(i, j));
                    level[i, j].SetEmpty(true);

                    //Debug
                    LevelDebug(level[i, j].position, GetCellPosition(i + 1, j));
                    LevelDebug(level[i, j].position, GetCellPosition(i, j + 1));
                }

            }
            //Debug
            LevelDebug(GetCellPosition(0, currentLevelDataScriptableObject.levelHeight), GetCellPosition(currentLevelDataScriptableObject.levelWidth, currentLevelDataScriptableObject.levelHeight));
            LevelDebug(GetCellPosition(currentLevelDataScriptableObject.levelWidth, 0), GetCellPosition(currentLevelDataScriptableObject.levelWidth, currentLevelDataScriptableObject.levelHeight));
            SetWorldTiles();
        }

        public bool isCellEmpty(int x, int z)
        {
            return level[x, z].isEmpty();
        }

        public void AddCellData(int x, int z)
        {
            if (!level[x, z].isEmpty())
                return;

          
            level[x, z].SetEmpty(false);
        }

        public void AddCellData(Vector3 position)
        {
            Vector2Int cellIndex = GetCellIndex(position);
            Debug.Log(cellIndex);
            if (!level[cellIndex.x, cellIndex.y].isEmpty())
                return;

            GameService.Instance.towerService.CreateTower(position);
            level[cellIndex.x, cellIndex.y].SetEmpty(false);
        }

        public void RemoveCellData(int x, int z)
        {
            if (!level[x, z].isEmpty())
                return;

            level[x, z].SetEmpty(true);
        }

        private Vector3 GetCellPosition(int x, int z)
        {
            return new Vector3(x * currentLevelDataScriptableObject.cellSize, 0f, z * currentLevelDataScriptableObject.cellSize);
        }

        private Vector2Int GetCellIndex(Vector3 position)
        {
            int x = Mathf.FloorToInt(position.x / currentLevelDataScriptableObject.cellSize);
            int z = Mathf.FloorToInt(position.z / currentLevelDataScriptableObject.cellSize);

            return new Vector2Int(x, z);
        }

        private Vector3 GetCellCenter(Vector3 cellPosition)
        {
            return cellPosition + new Vector3(currentLevelDataScriptableObject.cellSize, 0f, currentLevelDataScriptableObject.cellSize) * 0.5f;
        }


        public bool IsInBounds(Vector3 worldPosition)
        {
            int cellX = Mathf.FloorToInt(worldPosition.x / currentLevelDataScriptableObject.cellSize);
            int cellZ = Mathf.FloorToInt(worldPosition.z / currentLevelDataScriptableObject.cellSize);

            return (cellX >= 0 && cellX < currentLevelDataScriptableObject.levelWidth && cellZ >= 0 && cellZ < currentLevelDataScriptableObject.levelHeight);
        }

        public Vector3 GetCellPosition(Vector3 worldPosition)
        {
            int cellX = Mathf.FloorToInt(worldPosition.x / currentLevelDataScriptableObject.cellSize);
            int cellZ = Mathf.FloorToInt(worldPosition.z / currentLevelDataScriptableObject.cellSize);

            return GetCellCenter(level[cellX, cellZ].position);
        }

        public Cell GetCellData(Vector3 worldPosition)
        {
            if (!IsInBounds(worldPosition)) return null;

            int cellX = Mathf.FloorToInt(worldPosition.x / currentLevelDataScriptableObject.cellSize);
            int cellZ = Mathf.FloorToInt(worldPosition.z / currentLevelDataScriptableObject.cellSize);
            return level[cellX, cellZ];
        }

        public void SetWorldTiles()
        {
            CSVReader csvReader = new CSVReader(currentLevelDataScriptableObject.csvFile);
            string[,] dataString = csvReader.GetParsedCSV();
            for(int i = 0;i<dataString.GetLength(0);i++)
            {
                for (int j = 0; j < dataString.GetLength(0); j++)
                {
                    if (dataString[i,j] == "w" || dataString[i,j] == "w\r")
                    {
                        level[i, j].SetEmpty(false);

                        if (i == 0 && j == 0)
                        {
                            SpawnCornerWall(GetCellCenter(level[i, j].position));
                            continue;
                        }
                        else if (i == 0 && j == currentLevelDataScriptableObject.levelHeight - 1)
                        {
                            SpawnCornerWall(GetCellCenter(level[i, j].position));
                            continue;
                        }
                        else if (i == currentLevelDataScriptableObject.levelWidth - 1 &&                         
                            j == currentLevelDataScriptableObject.levelHeight - 1)
                        {
                            SpawnCornerWall(GetCellCenter(level[i, j].position));
                            continue;
                        }
                        else if (i == currentLevelDataScriptableObject.levelWidth - 1 &&
                            j == 0)
                        {
                            SpawnCornerWall(GetCellCenter(level[i, j].position));
                        }
                        var wall = Object.Instantiate(currentLevelDataScriptableObject.wallPrefab, GetCellCenter(level[i, j].position) ,Quaternion.identity);
                        if (i == 0 && j < currentLevelDataScriptableObject.levelHeight ||
                            i == currentLevelDataScriptableObject.levelWidth - 1 && j < currentLevelDataScriptableObject.levelHeight)
                        {
                            wall.transform.eulerAngles = new Vector3(0f, 90f, 0f);
                        }
                    }
                    if (dataString[i, j] == "b")
                    {
                        Object.Instantiate(currentLevelDataScriptableObject.emptyTile, GetCellCenter(level[i, j].position), Quaternion.identity);
                        level[i, j].SetEmpty(true);
                    }
                    if (dataString[i, j] == "p")
                    {
                        Object.Instantiate(currentLevelDataScriptableObject.pathTile, GetCellCenter(level[i, j].position), Quaternion.identity);
                        level[i, j].SetEmpty(false);
                    }
                    if (dataString[i,j] == "s")
                    {
                        Object.Instantiate(currentLevelDataScriptableObject.startTile, GetCellCenter(level[i, j].position), currentLevelDataScriptableObject.startTile.transform.rotation);
                        startPosition = GetCellCenter(level[i, j].position);
                        level[i, j].SetEmpty(false);
                    }
                    if (dataString[i, j] == "e")
                    {
                        Object.Instantiate(currentLevelDataScriptableObject.gate, GetCellCenter(level[i, j].position), currentLevelDataScriptableObject.gate.transform.rotation);
                        level[i, j].SetEmpty(false);
                    }
                }
            }
        }

        public void SpawnCornerWall(Vector3 position)
        {
           Object.Instantiate(currentLevelDataScriptableObject.cornerWallPrefab, position, Quaternion.identity);
        }


        private void LevelDebug(Vector3 start, Vector3 end)
        {
#if UNITY_EDITOR
            Debug.DrawLine(start, end, Color.green, 200f);
#endif
        }
    }
}


