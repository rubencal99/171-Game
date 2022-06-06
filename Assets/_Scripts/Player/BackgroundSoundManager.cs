using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FMODUnity;

public class BackgroundSoundManager : MonoBehaviour
{
    public SphereCollider detect;
    public FMODUnity.EventReference CombatMusicEvent;
    private FMOD.Studio.EventInstance CombatMusicInst;
    //[SerializeField]
    private int prisonerCount;
    private Dictionary<string, int> pDict = new Dictionary<string, int>(){
        {"Advanced Inmate", 0}, 
        {"Base Inmate 3D", 0},
        {"Improved Inmate 3D", 0},
        {"Inmate 3D", 0},
        {"Inmate", 0}, 
        {"Mutated Inmate", 0},
        {"Suicide Bomber 1", 0}
    };
    //[SerializeField]
    private int wardenCount;
    private Dictionary<string, int> wDict = new Dictionary<string, int>(){
        {"BaseAggressor", 0},
        {"ImprovedAggressor", 0},
        {"_Base Warden 3D", 0},
        {"Rambo Warden 3D", 0},
        {"SharpShooter Warden", 0},
        {"_Base Roomba", 0},
        {"Advanced Roomba", 0},
        {"Upgraded Roomba", 0},
        {"Upgraded Roomba Variant 1", 0},
        {"Upgraded Roomba Variant 2", 0}
    };
    //[SerializeField]
    private int supportCount;
    private Dictionary<string, int> sDict = new Dictionary<string, int>(){
        {"BufferEnemy 3D", 0},
        {"BaseBodyguard 3D", 0},
        {"ImprovedBodyguard 3D", 0},
        {"Base Medic 3D", 0},
        {"ImprovedMedic 3D", 0},
        {"MedicEnemy", 0}
    };
    [ParamRef]
    public string prisonerParam;
    [ParamRef]
    public string wardenParam;
    [ParamRef]
    public string supportParam;
    [ParamRef]
    public string bossParam;
    public Vector3 shopPos;
    public float shopDist;
    void Awake()
    {
        detect = GetComponent<SphereCollider>();

        prisonerCount = pDict.Sum(x => x.Value);
        wardenCount = wDict.Sum(x => x.Value);
        supportCount = sDict.Sum(x => x.Value); 
        // instantite music
        CombatMusicInst = FMODUnity.RuntimeManager.CreateInstance(CombatMusicEvent);
        CombatMusicInst.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
        CombatMusicInst.start();
    }
    void OnTriggerEnter(Collider collider)
    {
        string _name = collider.gameObject.name;
        // check for shopkeeper
        if (_name == "ShopKeeper")
        {
            shopPos = collider.gameObject.transform.position;
            // Debug.Log("shopkeeper at " + shopPos); 
        }

        // +1 to relevant enemy counter
        if (collider.tag != "Enemy")
        {
            return;
        }
        if (pDict.ContainsKey(_name)){
            pDict[_name] += 1;
            prisonerCount = pDict.Sum(x => x.Value);
        }
        else if (wDict.ContainsKey(_name)){
            wDict[_name] += 1;
            wardenCount = wDict.Sum(x => x.Value);
        }
        else if (sDict.ContainsKey(_name)){
            sDict[_name] += 1;
            supportCount = sDict.Sum(x => x.Value);
        }
    }

    void OnTriggerStay(Collider collider)
    {
        // If an enemy dies in range, -1 from its count

        if (collider.tag != "Enemy")
        {
            return;
        }
        Enemy enemy = collider.GetComponent<Enemy>();
        if (enemy == null)
        {
            return;
        }
    }
    void OnTriggerExit(Collider collider)
    {
        // -1 from relevant enemyCounter 

        string _name = collider.gameObject.name;
        if (collider.tag != "Enemy")
        {
            return;
        }
        if (pDict.ContainsKey(_name)){
            pDict[_name] -= 1;
            prisonerCount = pDict.Sum(x => x.Value);
        }
        else if (wDict.ContainsKey(_name)){
            wDict[_name] -= 1;
            wardenCount = wDict.Sum(x => x.Value);
        }
        else if (sDict.ContainsKey(_name)){
            sDict[_name] -= 1;
            supportCount = sDict.Sum(x => x.Value);
        }
    }

    private bool PrisonerCheck(GameObject _eCollider)
    {
        // iterate through dict of melee enemies, check if _enemy is in there)
        return false;
    }
    private bool WardenCheck(GameObject _eCollider)
    {
        // iterate through dict of ranged enemies, check if _enemy is in there)
        return false;
    }
    private bool SupportCheck(GameObject _eCollider)
    {
        // iterate through dict of support enemies, check if _enemy is in there)
        return false;
    }

    void FixedUpdate()
    {
        // if count > 0, increment relevant FMOD param by 0.1 until == 1
        // else, decrement relevant FMOD param by 0.1 until == 0 
        if (prisonerCount > 0){
            RuntimeManager.StudioSystem.setParameterByName(prisonerParam, 1.0f);
        }
        else {
            RuntimeManager.StudioSystem.setParameterByName(prisonerParam, 0.0f);
        }
        
        if (wardenCount > 0){
            RuntimeManager.StudioSystem.setParameterByName(wardenParam, 1.0f);
        }
        else {
            RuntimeManager.StudioSystem.setParameterByName(wardenParam, 0.0f);
        }

        if (supportCount > 0){
            RuntimeManager.StudioSystem.setParameterByName(supportParam, 1.0f);
        }
        else {
            RuntimeManager.StudioSystem.setParameterByName(supportParam, 0.0f);
        }

        // set shopDist and update ShopDistance param
        if (shopPos != null)
        {
            shopDist = Vector3.Distance(shopPos, transform.position);
            // Debug.Log("distance from shop: " + shopDist);
            CombatMusicInst.setParameterByName("ShopDistance", shopDist);
        }
    }

    void OnDestroy()
    {
        CombatMusicInst.release();
    }
}
