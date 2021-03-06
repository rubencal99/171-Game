using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// IAgent has not been implemented for player yet
public class Player : MonoBehaviour, IAgent, IHittable
{
    public static Player instance;
    public PlayerWeapon weaponParent;
    public ItemInventory inventory;
    public RoomNode currentRoom;
    [SerializeField]
    GameObject DronePrefab;
    public GameObject Drone;
    
    public GameObject DamageDisplay;

    public bool invincible = false;

   // public int damage_iframes = 20;


    public int damage_iframes = 200;

    [field: SerializeField]
    public float Health { get; set; } = 6;

     [field: SerializeField]
    public float MaxHealth { get; private set; } = 6f;

    [field: SerializeField]
    public int Wallet { get; private set; } = 80;

    [field: SerializeField]
    public int AugmentationTokens { get; private set; } = 0;

    [field: SerializeField]
    public int Damage { get; private set; }

    [field: SerializeField]
    public bool isDead;
    [field: SerializeField]
    public bool hasKey;

    [field: SerializeField]
    public float getHitFrequency;

    [field: SerializeField]
    public float getHitIntensity;

    [field: SerializeField]
    public float getHitTime;                           //For debug

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

     [field: SerializeField]
    public UnityEvent OnHeal { get; set; }

    [field: SerializeField]
    public GameObject DeathMenuUI;

    private Vector3 SpawnPosition;

    [SerializeField]
    private ParticleSystem blood;

    [SerializeField]
    private Image overlay;

    private bool HitLastFiveSec;
    private PlayerMovement playerMovement;

    //Defelction Shield logic
    private bool ShieldActivated = false;
    [SerializeField]
    private SphereCollider shield;

    private BoxCollider boxCollider;

    private AgentRenderer agentRenderer;
    [SerializeField]

     public Vector3 respawnPoint;
    public PlayerStateManager PlayerState; // game odject for agent input

    public bool grabbing = false;
    public bool hasGrabbed = false;
    public GameObject grabbedObject = null;
    public bool inWeaponZone = false;
    // private AgentInput w; // var to hold agent input

// =======
//     private AgentRenderer agentRender;
// >>>>>>> master

    private void Awake()
    {
        instance = this;
        PlayerSignaler.SetSignaler();
         //DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        // Note, resetting augmentations will want to be moved somewhere else
        // if incorporating multiple levels
        PlayerAugmentations.ResetAugmentations();
        SpawnPosition = transform.position;
        PlayerState = GetComponent<PlayerStateManager>();
        agentRenderer = GetComponentInChildren<AgentRenderer>();
        weaponParent = GetComponentInChildren<PlayerWeapon>();
        playerMovement = GetComponent<PlayerMovement>();
        //DeathMenuUI.SetActive(false);
        isDead = false;                                         //Debuging death
        hasKey = false;
        blood = GameObject.Find("PlayerBlood").GetComponent<ParticleSystem>();
       // overlay = GameObject.Find("Overlay").GetComponent<Image>();

        boxCollider = GetComponent<BoxCollider>();

        HitLastFiveSec = false;
        //shield = GameObject.Find("DeflectionShield").GetComponent<SphereCollider>();
    }

    void Update()
    {
        if (isDead==true){                      //For Debug the instance kill
             Health -= Health;
             OnDie?.Invoke();
             StartCoroutine(WaitToDie());
        }

        // if(HitLastFiveSec){
        //    // StartCoroutine(fadeOverlay());
        // }

         //raise defelction shield
        if(!ShieldActivated && Input.GetButton("Deflection Shield") && PlayerAugmentations.AugmentationList["DeflectionShield"] == true){
             StartCoroutine(RaiseShield());
        }

         //hipposkin
        if(PlayerAugmentations.AugmentationList["HippoSkin"] && !PlayerAugmentations.HippoApplied){
            StartCoroutine(ApplyHippo());
         }
         else if(!PlayerAugmentations.AugmentationList["HippoSkin"] && PlayerAugmentations.HippoApplied)
         {
             StartCoroutine(RemoveHippo());
         }
         if(PlayerAugmentations.AugmentationList["AutoDoc"] && PlayerAugmentations.AutoDocUsed == false){
            InvokeRepeating("RunAutoDoc",1f,2f);
            StartCoroutine(AutoDocCoolDown());
         }
         PlayerSignaler.Update();
    }

    public void setSpawnPoint(Vector3 spawn) {
        respawnPoint = spawn;
    }
    
    public void resetToSpawnPoint() {
        this.transform.position = respawnPoint;
    }
    public IEnumerator fadeOverlay(){
        var tempColor = overlay.color;
        var currHealth = (float)Health/MaxHealth;
        var tempAlpha = (float)(1f - currHealth);
        tempColor.a = tempAlpha;
        overlay.color = tempColor;
        yield return new WaitForSeconds(1f);
        // for(var alpha = tempAlpha; alpha > 0f; alpha -= 0.1f) {
        //     tempColor.a = alpha;
        //     overlay.color = tempColor;
        //     yield return null;
        // }
        for(float t = 1f; t > 0f; t -= 0.01f) {
            var newAlpha = Mathf.Lerp(tempAlpha, 0f, (1-t)* (1-t));
            tempColor.a = newAlpha;
            overlay.color = tempColor;
            yield return null;

        }
        tempColor.a = 0f;

        HitLastFiveSec = false;
    }

    public void Heal(float amount) {
        Health += amount;
        if(Health > MaxHealth)
            Health = MaxHealth;
    }

    public void setMaxHp(float amount) {
        MaxHealth = amount;
    }


    public void GetHit(float damage, GameObject damageDealer = null)
    {   
        if(invincible)
        {
            return;
        }
        //check if player is Dodging, if true, dont decrement health
        if (PlayerState.DiveState.diving) {
            return;
        }
        float d = PlayerSignaler.CallSecondSkin(damage);

        if(damageDealer != null)
            DamageType(damageDealer);

        //Damagedisplaying
        GameObject ds = null;
        if(DamageDisplay != null) {
            ds = Instantiate(DamageDisplay,transform.position,Quaternion.identity)as GameObject;
            ds.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        }

        Health -= d;
        HitLastFiveSec = true;
        blood.Play();
        //CameraShake.Instance.ShakeCamera((float)damage * getHitIntensity, getHitFrequency, getHitTime);
        if (Health > 0) {
            OnGetHit?.Invoke();
            StartCoroutine(iframes_damage());
        }
        else
        {
            if(PlayerSignaler.CallAngelsGrace()){
                UseGrace();
            }
            OnDie?.Invoke();
            StartCoroutine(WaitToDie());
        }
    }

    public void DamageType(GameObject damageDealer)
    {
        if(damageDealer.GetComponent<Bullet>())
        {
            BulletDataSO bulletData = damageDealer.GetComponent<Bullet>().BulletData;
            playerMovement.Knockback(bulletData.KnockbackDuration, bulletData.KnockbackPower, -damageDealer.GetComponent<Bullet>().direction);
        }
        else if(damageDealer.GetComponent<Melee>())
        {
            MeleeDataSO meleeData = damageDealer.GetComponent<Melee>().meleeData;
            var weaponPosition = damageDealer.transform.parent.position;
            var direction = transform.position - weaponPosition;
            playerMovement.Knockback(meleeData.KnockbackDelay, meleeData.KnockbackPower, -direction);
        }
    }


    public void Heal()
    {
        Health += 2;
    }

    public void AddBounty(int funds)
    {
        Wallet += funds;
    }

    public bool CanPurchase(int cost)
    {
        if (cost > Wallet)
        {
            return false;
        }
        return true;
    }

    public void Purchase(int cost)
    {
        Wallet -= cost;
    }

    public bool CanAcuire(int cost)
    {
        if (AugmentationTokens < cost)
        {
            return false;
        }
        return true;
    }

    public void Acquire(int cost)
    {
        AugmentationTokens -= cost;
    }
    IEnumerator WaitToDie(){
        gameObject.layer = 0;
        agentRenderer.isDying = true;
        isDead = true;
        yield return new WaitForSeconds(0.5f);
        //Destroy(gameObject);
        // Play End Game Screen here
        DeathMenuUI.SetActive(true);
    }


    public void Respawn()
    {
        transform.position = SpawnPosition;
    }

    public void RunAutoDoc(){
        int toHeal = Mathf.CeilToInt(PlayerAugmentations.AutoDocHeal * MaxHealth);
        Heal(toHeal);
        PlayerAugmentations.AutoDocUsed = true;
    }

     public IEnumerator iframes_damage() {
        invincible = true;
        yield return new WaitForSeconds((float)damage_iframes / 60f);
        invincible = false;
    }

    public IEnumerator RaiseShield(){
        Debug.Log("raise shield");
        ShieldActivated = true;
        shield.enabled = true;
        //Maybe get an animation here
        yield return new WaitForSeconds(PlayerAugmentations.DefelctionTime);
        shield.enabled = false;
        ShieldActivated = false;
    }

    public IEnumerator ApplyHippo(){
        PlayerAugmentations.HippoApplied = true;
        yield return null;
        setMaxHp(MaxHealth + PlayerAugmentations.HippoHealth);
        Debug.Log("HippoApplied from applyHippo: " + PlayerAugmentations.HippoApplied + PlayerSignaler.BuffHippo());
    }
    public IEnumerator RemoveHippo(){
        PlayerAugmentations.HippoApplied = false;
        yield return null;
        setMaxHp(MaxHealth - PlayerAugmentations.HippoHealth - PlayerSignaler.BuffHippo());
        Debug.Log("Hippo Removed from RemoveHippo: " + PlayerAugmentations.HippoApplied);
    }

    public IEnumerator AutoDocCoolDown(){
        yield return new WaitForSeconds(PlayerAugmentations.AutoDocCoolDown);
        PlayerAugmentations.AutoDocUsed = false;
    }

    public void InstantiateDrone()
    {
        Vector3 spawn = new Vector3(transform.position.x, 1, transform.position.z);
        Drone = Instantiate(DronePrefab, spawn, DronePrefab.transform.rotation);
    }

    public void DestroyDrone()
    {
        Destroy(Drone);
    }

    private void OnApplicationQuit()
    {
        inventory.ClearInventory();
    }

    public IEnumerator UseGrace(){
        Heal(MaxHealth);
        boxCollider.enabled = false;
        // remove object from inventory
        inventory.AContainer[1].Clear(); //could be an error

        yield return new WaitForSeconds(PlayerAugmentations.GracePeriod);
        boxCollider.enabled = true;
    }
}
