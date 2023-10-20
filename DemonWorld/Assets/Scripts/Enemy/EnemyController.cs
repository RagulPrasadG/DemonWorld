using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController
{
    public EnemyView enemyView;
    private List<Vector3> wayPoints;
    private int currentWayPointIndex;
    private EnemyDataScriptableObject enemyDataScriptableObject;
    private float currentHealth;

    public void Init(EnemyDataScriptableObject enemyData)
    {
        this.enemyDataScriptableObject = enemyData;
        this.enemyView = Object.Instantiate(enemyData.enemyView,GameService.Instance.GetLevelService().GetStartPosition(),
        enemyData.enemyView.transform.rotation);
        this.enemyView.SetController(this);
        this.wayPoints = GameService.Instance.GetLevelService().GetCurrentLevelWayPoints();
        this.currentHealth = enemyData.maxHealth;
        
    }
   
   
    public void Move()
    {
        if (Vector3.Distance(this.enemyView.transform.position,wayPoints[currentWayPointIndex]) <= 0.2f)
        {
            if(currentWayPointIndex >= wayPoints.Count - 1)
            {
                ResetAllWayPoints();
                GameService.Instance.GetWaveService().ReturnEnemyToPool(this);
                this.enemyView.gameObject.SetActive(false);
                return;
            }
            currentWayPointIndex++;
        }

        TurnTo(wayPoints[currentWayPointIndex]);
        this.enemyView.transform.position = Vector3.MoveTowards(this.enemyView.transform.position, wayPoints[currentWayPointIndex],
            enemyDataScriptableObject.speed * Time.deltaTime);
    }

    private void ResetAllWayPoints() => currentWayPointIndex = 0;
 
    public void TakeDamage(float damageAmount)
    {
        Debug.Log("TakenDamage!");
        if(currentHealth <= 0)
        {
            GameService.Instance.GetSoundService().PlaySfx(SoundType.EnemyDie);
            GameService.Instance.GetVfxService().PlayVfx(VfxType.GoblinDeath, this.enemyView.target.position);
            ResetAllWayPoints();
            GameService.Instance.GetWaveService().ReturnEnemyToPool(this);
            this.enemyView.eventService.OnEnemyDie.RaiseEvent(this);
            this.currentHealth = enemyDataScriptableObject.maxHealth;
            this.enemyView.gameObject.SetActive(false);
        }
        else
        {
            currentHealth -= damageAmount;
            GameService.Instance.GetVfxService().PlayVfx(VfxType.GoblinDamage, this.enemyView.target.position);
        }
    }

    private void TurnTo(Vector3 position)
    {
        Vector3 turnDirection = wayPoints[currentWayPointIndex] - this.enemyView.transform.position;
        Quaternion turnRotation = Quaternion.LookRotation(turnDirection);
        this.enemyView.transform.rotation = Quaternion.Slerp(this.enemyView.transform.rotation, turnRotation,
            enemyDataScriptableObject.turnSpeed * Time.deltaTime);
        
    }


}
