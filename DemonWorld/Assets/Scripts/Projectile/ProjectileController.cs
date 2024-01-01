using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController
{
    public ProjectileView projectileView;
    public ProjectileData projectileData;

    public ProjectileController(ProjectileData projectileData)
    {
        this.projectileData = projectileData;
        this.projectileView = Object.Instantiate(projectileData.projectileView);
        this.projectileView.SetController(this);
    }
    
    public void FireTo(Vector3 from,Vector3 to)
    {
        projectileView.rigidBody.velocity = Vector3.zero;
        projectileView.gameObject.SetActive(true);
        Vector3 direction = to - from;
        Quaternion turnRotation = Quaternion.LookRotation(direction);
        this.projectileView.transform.position = from;
        this.projectileView.transform.rotation = turnRotation;
        projectileView.rigidBody.velocity = direction.normalized * projectileData.speed * Time.deltaTime;
    }

    public void OnProjectileHit()
    {
        GameService.Instance.towerService.ReturnProjectileToPool(this);
        this.projectileView.gameObject.SetActive(false);
    }

}
