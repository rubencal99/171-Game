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
    private Dictionary<GameObject, string> roomDict = new Dictionary<GameObject, string>();
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
        if (collider.tag != "Enemy"){
            return;
        }
        if (roomDict.ContainsKey(collider.gameObject)){
            return;
        }
        Enemy _enemy = collider.GetComponent<Enemy>();
        if (_enemy == null){
            return;
        }

        if (!_enemy.hasDied)
        {
            roomDict.Add(collider.gameObject, _name);
            UpdateDicts();
        }  
    }
    void OnTriggerStay(Collider collider)
    {
        // check for dead guys, remove them from roomDict
        if (collider.tag != "Enemy"){
            return;
        }
        if (!roomDict.ContainsKey(collider.gameObject)){
            return;
        }
        Enemy _enemy = collider.GetComponent<Enemy>();
        if (_enemy == null){
            return;
        }
        if (_enemy.hasDied)
        {
            roomDict.Remove(collider.gameObject);
            UpdateDicts(collider.gameObject.name);
        }
    }
    void OnTriggerExit(Collider collider)
    {
        // -1 from relevant enemyCounter 
        if (collider.tag != "Enemy"){
            return;
        }

        if (roomDict.ContainsKey(collider.gameObject))
        {
            roomDict.Remove(collider.gameObject);
            UpdateDicts(collider.gameObject.name);
        }
    }
    private void UpdateDicts(string zeroKey = null)
    {
        // Updates pDict, wDict, and sDict based on the contents of roomDict
        Dictionary<string, int> _headcountD = new Dictionary<string, int>();
        foreach (KeyValuePair<GameObject, string> e in roomDict)
        {
            if (!_headcountD.ContainsKey(e.Value)){
                _headcountD.Add(e.Value, 0);  
            }
            _headcountD[e.Value] += 1;
        }
        if (zeroKey != null){
            if (pDict.ContainsKey(zeroKey) && pDict[zeroKey] == 1){
                pDict[zeroKey] = 0;
            }
            if (wDict.ContainsKey(zeroKey) && wDict[zeroKey] == 1){
                wDict[zeroKey] = 0;
            }
            if (sDict.ContainsKey(zeroKey) && sDict[zeroKey] == 1){
                sDict[zeroKey] = 0;
            }
        }
        foreach (KeyValuePair<string, int> i in _headcountD)
        {
            if (pDict.ContainsKey(i.Key)){
                pDict[i.Key] = _headcountD[i.Key];
            }
            else if (wDict.ContainsKey(i.Key)){
                wDict[i.Key] = _headcountD[i.Key];
            }
            else if (sDict.ContainsKey(i.Key)){
                sDict[i.Key] = _headcountD[i.Key];
            }
        }
        prisonerCount = pDict.Sum(x => x.Value);
        wardenCount = wDict.Sum(x => x.Value);
        supportCount = sDict.Sum(x => x.Value);
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
