using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Extends AgentWeapon so we can use the functions from it
public class PlayerWeapon : AgentWeapon
{
    private float timeToReload = 0.0f;

    public int selectedWeapon = 0;
    public int numGrenades = 5;
    public GameObject Grenade;

    Vector3 mousePos;


    private void Start()
    {
        SelectWeapon();
        InfAmmo = false;
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
            //DisplayWeapon.Instance.UpdateWeapon();
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

    public void ThrowItem()
    {
        if (numGrenades > 0)
        {
            Debug.Log("Item Thrown");
            SpawnItem(transform.position, transform.rotation);
        }
    }

    private void SpawnItem(Vector3 position, Quaternion rotation)
    {
        Debug.Log("Before instantiate");
        var itemPrefab = Instantiate(Grenade, position, rotation);
        Debug.Log("After instantiate");
    }
}
