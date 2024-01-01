using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "NewEventService",menuName = "Data/NewEventService")]
public class EventServiceScriptableObject : ScriptableObject
{
    [Header("Gameplay")]
    public VoidEvent OnStartGame = new VoidEvent();
    public VoidEvent OnExitGame = new VoidEvent();
    public GenericEvent<float> OnProjectileHitEnemy = new GenericEvent<float>();
    public GenericEvent<EnemyController> OnEnemyDie = new GenericEvent<EnemyController>();

    [Header("UI")]
    public GenericEvent<TowerType> OnTowerSelected = new GenericEvent<TowerType>();
}


public class VoidEvent
{
    private Action baseEvent;
    public void AddListener(Action listener)
    {
        baseEvent += listener;
    }
    public void RemoveListener(Action listener)
    {
        baseEvent -= listener;
    }

    public void RaiseEvent()
    {
        baseEvent?.Invoke();
    }
}


public class GenericEvent<T>
{
    private Action<T> baseEvent;
    public void AddListener(Action<T> listener)
    {
        baseEvent += listener;
    }
    public void RemoveListener(Action<T> listener)
    {
        baseEvent -= listener;
    }

    public void RaiseEvent(T param)
    {
        baseEvent?.Invoke(param);
    }
}

