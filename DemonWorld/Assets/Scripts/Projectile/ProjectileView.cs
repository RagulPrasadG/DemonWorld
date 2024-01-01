using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileView : MonoBehaviour
{
    [SerializeField] EventServiceScriptableObject eventService;
    private ProjectileController projectileController;

    public Rigidbody rigidBody;
    

    public void SetController(ProjectileController controller) => this.projectileController = controller;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Level"))
        {
            EnemyView enemyView = other.GetComponent<EnemyView>();
            if (enemyView != null)
            {
                enemyView.enemyController.TakeDamage(projectileController.projectileData.damageAmount);
            }
            projectileController.OnProjectileHit();
           
        }
        
    }

}
