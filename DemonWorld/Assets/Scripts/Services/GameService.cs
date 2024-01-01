using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demonworld.Services;

public class GameService : Singleton<GameService>
{
    [SerializeField] AudioSource sfxAudioSource;
    [SerializeField] AudioSource BgmAudioSource;

    #region Services
    public PreviewService previewService;
    public UIService UIService;
    private LevelService levelService;
    private WaveService waveService;
    private VfxService vfxService;
    private SoundService soundService;
    public TowerService towerService { get; private set; }
    #endregion

    #region Stats
    public int coins;
    public float health;
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
    [SerializeField] SoundServiceScriptableObject soundDataScriptableObject;
    #endregion


    private void Start()
    {
        levelService = new LevelService(levelDataScriptableObjects);
        towerService = new TowerService(towerDataScriptableObject,projectileDataScriptableObject);
        waveService = new WaveService(enemyDataScriptableObjects,waveDataScriptableObject,eventServiceScriptableObject);
        vfxService = new VfxService(vfxDataScriptableObject);
        soundService = new SoundService(soundDataScriptableObject, sfxAudioSource, BgmAudioSource);
        previewService.Init(levelService);
        UIService.Init(towerService, previewService);
        SetEvents();
    }

    public void StartWave()
    {
        soundService.PlayBGM(SoundType.BattleMusic, true);
        StartCoroutine(waveService.StartWave());
    }

    public void DecreaseCoinAmount(int amount)
    {
        this.coins -= amount;
        UIService.SetCoinText(amount);
    }

    public void SetEvents()
    {
        eventServiceScriptableObject.OnStartGame.AddListener(OnStartGame);
    }

    public void IncreaseCoinAmount(int amount)
    {
        this.coins += amount;
        UIService.SetCoinText(amount);
    }

    public void IncreaseHealth(int value)
    {
        this.health += value;
        UIService.SetHealthBar(health);
    }

    public void ReduceHealth(float value)
    {
        this.health -= value;
        UIService.SetHealthBar(health);
    }
    
    public LevelService GetLevelService() => levelService;
    public WaveService GetWaveService() => waveService;

    public TowerService GetTowerService() => towerService;

    public SoundService GetSoundService() => soundService;
    public VfxService GetVfxService() => vfxService;

    public void OnStartGame()
    {
        levelService.CreateLevel();
    }

}
