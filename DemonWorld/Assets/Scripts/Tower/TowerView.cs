using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerView : MonoBehaviour
{
    [SerializeField] Animator animator;
    public EventServiceScriptableObject eventService;
    public Transform defenderTransform;
    public Transform attackPoint;
    private TowerController towerController;

    public void SetController(TowerController heroController)
    {
        this.towerController = heroController;
    }

    private void OnEnable()
    {
        eventService.OnEnemyDie.AddListener(RemoveEnemy);
    }

    private void OnDisable()
    {
        eventService.OnEnemyDie.RemoveListener(RemoveEnemy);
    }

    public void Update()
    {
        towerController.Attack();    
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyView enemyView = other.gameObject.GetComponent<EnemyView>();
        if (enemyView != null)
        {
            towerController.enemiesInRange.Add(enemyView);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        EnemyView enemyView = other.gameObject.GetComponent<EnemyView>();
        if (enemyView != null)
        {
            towerController.enemiesInRange.Remove(enemyView);
        }
    }

    public void RemoveEnemy(EnemyController enemyController)
    {
        towerController.enemiesInRange.Remove(enemyController.enemyView);
    }

    public void PlayAnimation(string animationName)
    {
        animator.SetTrigger(animationName);
    }


}
