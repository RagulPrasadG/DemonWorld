using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public EventServiceScriptableObject eventService;

    private EnemyController enemyController;
    public ParticleSystem destoryEffect;


    public void SetController(EnemyController enemyController) => this.enemyController = enemyController;

    private void Update()
    {
        enemyController.Move();
    }

    public void TakeDamage(float damageAmount)
    {
        destoryEffect.Play();
        enemyController.TakeDamage(damageAmount);
    }
    
}
