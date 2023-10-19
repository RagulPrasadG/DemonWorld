using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demonworld.Services;

public class GameService : Singleton<GameService>
{
    #region Services
    public PreviewService previewService;
    public UIService UIService;
    private LevelService levelService;
    private WaveService waveService;
    private VfxService vfxService;
    public TowerService towerService { get; private set; }
    #endregion

    #region Stats
    public int coins;
    public int health;
    #endregion

    #region ScriptableObjects
    [Header("LEVEL_DATA")]
    [Space(10)]
    [SerializeField] List<LevelDataScriptableObject> levelDataScriptableObjects;

    [Header("ENEMY_DATA")]
    [Space(10)]
    [SerializeField] List<EnemyDataScriptableObject> enemyDataScriptableObjects;
    [SerializeField] TowerDataScriptableObject towerDataScriptableObject;
    [SerializeField] WaveDataScriptableObject waveDataScriptableObject;
    [SerializeField] ProjectileDataScriptableObject projectileDataScriptableObject;
    [SerializeField] EventServiceScriptableObject eventServiceScriptableObject;
    [SerializeField] VfxDataScriptableObject vfxDataScriptableObject;
    #endregion


    private void Start()
    {
        levelService = new LevelService(levelDataScriptableObjects);
        towerService = new TowerService(towerDataScriptableObject,projectileDataScriptableObject);
        waveService = new WaveService(enemyDataScriptableObjects,waveDataScriptableObject);
        vfxService = new VfxService(vfxDataScriptableObject);
        previewService.Init(levelService);
        UIService.Init(towerService, previewService);
       
        
    }

    public void StartWave() => StartCoroutine(waveService.StartWave());


    
    public LevelService GetLevelService() => levelService;
    public WaveService GetWaveService() => waveService;

    public TowerService GetTowerService() => towerService;

    public VfxService GetVfxService() => vfxService;
}
