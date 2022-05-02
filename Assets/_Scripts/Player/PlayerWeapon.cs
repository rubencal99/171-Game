using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Extends AgentWeapon so we can use the functions from it
public class PlayerWeapon : AgentWeapon
{
    public static PlayerWeapon instance;
    private float timeToReload = 0.0f;

    public GameObject selectedWeapon;
    public int numGrenades = 5;
    public GameObject Grenade;

    public GameObject Primary;
    public GameObject Secondary;
    private GameObject itemPrefab;

    public RenderThrowableArc throwableArc;

    public Vector3 mousePos;
    private ItemInventory itemInventory;
    public bool useInventory;

    void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        itemInventory = transform.parent.GetComponent<Player>().inventory;
        CheckInventory();
        if(Primary)
        {
            Primary.SetActive(true);
        }
        if(Secondary)
        {
            Secondary.SetActive(false);
        }
        InfAmmo = false;
        AssignWeapon();
    }


    /*private void Update()
    {

        GameObject previousSelectedWeapon = selectedWeapon;

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
            Primary.SetActive(true);
            Secondary.SetActive(false);
            Primary.GetComponent<IWeapon>().ForceReload();
            selectedWeapon = Primary;
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
            Primary.SetActive(false);
            Secondary.SetActive(true);
            Secondary.GetComponent<IWeapon>().ForceReload();
            selectedWeapon = Secondary;
        }
        if (previousSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
            DisplayWeapon.Instance.UpdateWeapon();
        }
    }*/

    public void CheckInventory()
    {
        WeaponSlot slot1 = itemInventory.WContainer[0];
        WeaponSlot slot2 = itemInventory.WContainer[1];
        if(slot1.item != null)
        {
            Debug.Log("Before instantiate item");
            PlayerInventory.instance.InstantiateItem(slot1.item);
            slot1.ReplacePrimary(slot1.item.prefabClone);
            
        }
        else if(useInventory)
        {
            Primary = null;
        }
        if(slot2.item != null)
        {
            PlayerInventory.instance.InstantiateItem(slot2.item);
            slot2.ReplaceSecondary(slot2.item.prefabClone);
        }
        else if(useInventory)
        {
            Secondary = null;
        }

    }

    public void TogglePrimary()
    {
        if(Primary && !Primary.activeSelf)
        {
            Primary.SetActive(true);
            if(Secondary != null)
                Secondary.SetActive(false);
            Primary.GetComponent<IWeapon>().ForceReload();
            AssignWeapon();
        }
    }

    public void ToggleSecondary()
    {
        if(Secondary && !Secondary.activeSelf)
        {
            if(Primary != null)
                Primary.SetActive(false);
            Secondary.SetActive(true);
            Secondary.GetComponent<IWeapon>().ForceReload();
            AssignWeapon();
        }
    }

    private void SelectWeapon()
    {
        /*int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                weapon.GetComponent<IWeapon>().ForceReload();
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }*/


        AssignWeapon();
    }

    public void prepThrow()
    {
        if (numGrenades > 0)
        {
            Debug.Log("throw prepped");
            SpawnItem(transform.position, transform.rotation);
            //throwableArc.SetArcAngle(this.desiredAngle);
        }
    }
    public void ThrowItem()
    {
            Debug.Log("Item Thrown");
            itemPrefab.GetComponent<_BaseThrowable>().Thrown = true;
            itemPrefab.GetComponent<_BaseThrowable>().addForce();
            
    }


    private void SpawnItem(Vector3 position, Quaternion rotation)
    {
        Debug.Log("Before instantiate");
        itemPrefab = Instantiate(Grenade, position, rotation);
        itemPrefab.transform.parent = this.transform;
        Debug.Log("After instantiate");
    }
}
