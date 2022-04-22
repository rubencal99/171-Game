using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerProgressManager
{
  
   public static PlayerWeapon currentPlayerWeaponState = null;
   public static Player currentPlayerState = null;

   public static bool hasData = false;

   public static string PlayerJsonFormat = null;
   public static string PlayerWeaponJsonFormat = null;

   public static int primaryCurrentAmmo = 0;

   public static int primaryTotalAmmo = 0;

   public static int secondaryCurrentAmmo = 0;

   public static int secondaryTotalAmmo = 0;
   public static void SavePlayer(PlayerWeapon pw, Player ps) {
       currentPlayerWeaponState = pw;
       currentPlayerState = ps;
      
       PlayerJsonFormat = JsonUtility.ToJson(ps.GetComponent<Player>());
    //    var primary = pw.Primary.GetComponent<Gun>();
    //    var secondary = pw.Secondary.GetComponent<Gun>();
    //    primaryCurrentAmmo = primary.Ammo;
    //    primaryTotalAmmo = primary.TotalAmmo;

    //    secondaryCurrentAmmo = secondary.Ammo;
    //    secondaryTotalAmmo = secondary.TotalAmmo;
       //PlayerWeaponJsonFormat = JsonUtility.ToJson(pw.GetComponent<PlayerWeapon>());
       hasData = true; 
        Debug.Log("Current Player State Saved, JSON = " + PlayerWeaponJsonFormat);

   }
   
   public static void LoadPlayer(GameObject pw, GameObject ps) {
       JsonUtility.FromJsonOverwrite(PlayerJsonFormat, ps.GetComponent<Player>());
       Debug.Log("Player State Loaded");
    //   JsonUtility.FromJsonOverwrite(PlayerWeaponJsonFormat, pw.GetComponent<PlayerWeapon>());
   }

}
