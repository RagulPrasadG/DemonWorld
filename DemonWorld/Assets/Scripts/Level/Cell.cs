using UnityEngine;

public class Cell
{
    public Vector3 position;
    public TowerController towerController;
    private bool IsEmpty;
    public Cell(Vector3 position)
    {
        this.position = position;     
    }

    public bool isEmpty() => this.IsEmpty;
    public void SetEmpty(bool toggle) => IsEmpty = toggle;

}