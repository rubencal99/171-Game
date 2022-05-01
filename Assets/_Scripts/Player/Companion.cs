using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : Enemy
{
    public Enemy enemyTarget;
    public Player playerFollow;

    void Awake()
    {
      enemyTarget = null;
      playerFollow = Player.instance;
    }
}
