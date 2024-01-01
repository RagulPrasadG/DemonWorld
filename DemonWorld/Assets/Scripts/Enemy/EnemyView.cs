using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public AudioSource audioSource;
    public EventServiceScriptableObject eventService;
    public Transform target;
    public EnemyController enemyController;


    public void SetController(EnemyController enemyController) => this.enemyController = enemyController;

    private void Update()
    {
        enemyController.Move();
    }


}
