using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public AudioSource audioSource;
    public EventServiceScriptableObject eventService;
    public Transform target;
    private EnemyController enemyController;


    public void SetController(EnemyController enemyController) => this.enemyController = enemyController;

    private void Update()
    {
        enemyController.Move();
    }

    private void OnEnable()
    {
        GameService.Instance.GetSoundService().PlaySfxAt(SoundType.EnemySpawn,this.audioSource);
    }


    public void TakeDamage(float damageAmount)
    {
        enemyController.TakeDamage(damageAmount);
    }
    
}
