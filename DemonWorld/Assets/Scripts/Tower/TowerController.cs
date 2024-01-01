using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController
{
    private TowerView towerView;
    private TowerData towerData;
    private ProjectileDataScriptableObject projectileDataScriptableObject;
    private float attackInterval;
    private GameService gameService;

    public List<EnemyView> enemiesInRange;

    public TowerController(GameService gameService,
        TowerData towerData, ProjectileDataScriptableObject projectileDataScriptableObject, Vector3 position)
    {
        this.gameService = gameService;
        this.towerView = Object.Instantiate(towerData.heroView);
        this.towerView.transform.position = position;
        this.towerData = towerData;
        this.projectileDataScriptableObject = projectileDataScriptableObject;
        this.towerView.SetController(this);
        this.enemiesInRange = new List<EnemyView>();
    }

    public void Turn()
    {
        Vector3 direction = enemiesInRange[0].transform.position - towerView.transform.position;
        Quaternion turnRotation = Quaternion.LookRotation(direction);
        this.towerView.defenderTransform.rotation = Quaternion.Slerp(this.towerView.transform.rotation, turnRotation,
            towerData.rotationSpeed * Time.deltaTime);
    }

    public void Attack()
    {
        if (enemiesInRange.Count == 0)
            return;


        Turn();

        if(attackInterval >= towerData.attackRate)
        {
            ProjectileController projectile = GameService.Instance.towerService.projectilePool.GetProjectile(
            projectileDataScriptableObject.GetProjectileData(towerData.towerType));
            this.towerView.PlayAnimation("Attack");
            projectile.FireTo(towerView.attackPoint.position, enemiesInRange[0].target.position);
            PlayAttackSound();
            attackInterval = 0;
            
        }
        attackInterval += Time.deltaTime;


    }

    public void PlayAttackSound()
    {
        switch (towerData.towerType)
        {
            case TowerType.WizardTower:
                GameService.Instance.soundService.PlaySfxAt(SoundType.WizardFire,this.towerView.audioSource);
                break;
            case TowerType.CrossBowTower:
                GameService.Instance.soundService.PlaySfxAt(SoundType.CrossBowFire, this.towerView.audioSource);
                break;
        }

    }

}
