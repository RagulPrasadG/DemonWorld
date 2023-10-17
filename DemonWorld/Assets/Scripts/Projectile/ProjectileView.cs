using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileView : MonoBehaviour
{
    [SerializeField] EventServiceScriptableObject eventService;
    public Rigidbody rigidBody;
    private ProjectileController projectileController;
    public void SetController(ProjectileController controller) => this.projectileController = controller;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Level"))
        {
            Debug.Log("Level");
            EnemyView enemy = other.GetComponent<EnemyView>();
            if (enemy != null)
            {
                Debug.Log("Enemy");
                enemy.TakeDamage(projectileController.projectileData.damageAmount);
            }
            projectileController.OnProjectileHit();
           
        }
        
    }

}
