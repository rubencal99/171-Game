using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Extends AgentWeapon so we can use the functions from it
public class PlayerWeapon : AgentWeapon
{

    private float timeToReload = 0.0f;
    // Nothing in here because Player is the only one with weapons for now
    // If we want to give enemies weapons that behave differently, we'd make an EnemyWeapon class
    // that extends AgentWeapon
    // But basic aiming and shooting logic should be inhereted from the same parent class

    public void displayReloadProgressBar() {
       var reloadBar = this.transform.parent.GetComponentInChildren<PlayerReload>();
       reloadBar.displayReloadProgressBar();
       
    }

}
