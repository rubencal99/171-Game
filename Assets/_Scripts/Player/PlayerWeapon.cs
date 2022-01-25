using System;
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

    public int selectedWeapon = 0;

    private void Start()
    {
        SelectWeapon();
    }


    private void Update()
    {

        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
            DisplayWeapon.Instance.UpdateWeapon();
        }
    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }


        AssignWeapon();
    }
}
