using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : Enemy
{
    public static Companion instance;
    public Enemy enemyTarget;
    public Player playerFollow;
    public AgentWeapon weaponParent;

    public GameObject defaultWeapon;
    public GameObject upgradedWeapon;
    public float ROFMultiplier;
    public float SpeedMultiplier;

    void Awake()
    {
      instance = this;
      enemyTarget = null;
      playerFollow = Player.instance;
    }

    public override void Update(){
      DroneSignaler.Update();

    }
}
