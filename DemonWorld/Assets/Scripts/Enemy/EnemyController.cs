using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController
{
    public  EnemyView view;
    private List<Vector3> wayPoints;
    private int currentWayPointIndex;
    private EnemyDataScriptableObject enemyDataScriptableObject;
    private float currentHealth;

    private GameService Gameservice;

    public void Init(GameService gameService,EnemyDataScriptableObject enemyData)
    {
        this.Gameservice = gameService;
        this.enemyDataScriptableObject = enemyData;
        this.view = Object.Instantiate(enemyData.enemyView, gameService.levelService.GetStartPosition(),
        enemyData.enemyView.transform.rotation);
        this.view.SetController(this);
        this.wayPoints = Gameservice.levelService.GetCurrentLevelWayPoints();
        this.currentHealth = enemyData.maxHealth;
        Gameservice.soundService.PlaySfxAt(SoundType.EnemySpawn, this.view.audioSource);
    }
   
   
    public void Move()
    {
        if (Vector3.Distance(this.view.transform.position,wayPoints[currentWayPointIndex]) <= 0.2f)
        {
            if(currentWayPointIndex >= wayPoints.Count - 1)
            {
                ResetAllWayPoints();
                Gameservice.waveService.ReturnEnemyToPool(this);
                this.view.gameObject.SetActive(false);
                return;
            }
            currentWayPointIndex++;
        }

        TurnTo(wayPoints[currentWayPointIndex]);
        this.view.transform.position = Vector3.MoveTowards(this.view.transform.position, wayPoints[currentWayPointIndex],
            enemyDataScriptableObject.speed * Time.deltaTime);
    }

    private void ResetAllWayPoints() => currentWayPointIndex = 0;
 
    public void TakeDamage(float damageAmount)
    {
        if(currentHealth <= 0)
        {
            Gameservice.IncreaseCoinAmount(enemyDataScriptableObject.coinsForDeath);
            Gameservice.soundService.PlaySfx(SoundType.EnemyDie);
            Gameservice.vfxService.PlayVfx(VfxType.GoblinDeath, this.view.target.position);
            ResetAllWayPoints();
            Gameservice.waveService.ReturnEnemyToPool(this);
            this.view.eventService.OnEnemyDie.RaiseEvent(this);
            this.currentHealth = enemyDataScriptableObject.maxHealth;
            this.view.gameObject.SetActive(false);
        }
        else
        {
            currentHealth -= damageAmount;
            Gameservice.vfxService.PlayVfx(VfxType.GoblinDamage, this.view.target.position);
        }
    }

    private void TurnTo(Vector3 position)
    {
        Vector3 turnDirection = wayPoints[currentWayPointIndex] - this.view.transform.position;
        Quaternion turnRotation = Quaternion.LookRotation(turnDirection);
        this.view.transform.rotation = Quaternion.Slerp(this.view.transform.rotation, turnRotation,
            enemyDataScriptableObject.turnSpeed * Time.deltaTime);
        
    }

    public void SetPosition(Vector3 position) => this.view.transform.position = position;
    public void SetRotation(Quaternion rotation) => this.view.transform.rotation = rotation;


}
