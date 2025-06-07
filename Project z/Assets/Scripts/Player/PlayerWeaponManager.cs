using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct WeaponSlotConfig
{
    public WeaponType weaponType;
    public Transform handTransform;
    public GameObject weaponPrefab;

    // THESE THINGS WEER THERE ONCE UPON A TIME
    // public float weaponOffsetX;
    // public float weaponOffsetY;
    // public float weaponOffsetZ;
    // public float localWeaponRotationX;
    // public float localWeaponRotationY;
    // public float localWeaponRotationZ;

}

public class PlayerWeaponManager : MonoBehaviour
{
    private Dictionary<WeaponType, Weapon> slots;
    private Dictionary<WeaponType, GameObject> spawnedWeapons;
    private Dictionary<WeaponType, WeaponSlotConfig> weaponSlotConfigs;
    private WeaponType currentEquipped;
    private PlayerAnimation playerAnimation;
    private PlayerController player;

    // SLOT CONFIGURATION
    [SerializeField] public List<WeaponSlotConfig> SlotConfigs;


    private void Awake()
    {
        playerAnimation = GetComponentInParent<PlayerAnimation>();
        player = GetComponentInParent<PlayerController>();

        weaponSlotConfigs = new();
        spawnedWeapons = new();

        foreach (var slotConfig in SlotConfigs)
        {
            weaponSlotConfigs.Add(slotConfig.weaponType, slotConfig);
            spawnedWeapons.Add(slotConfig.weaponType, null);
        }

        slots = new Dictionary<WeaponType, Weapon>()
        {
            { WeaponType.Barehands, new Weapon("w0", "Bare Hands", WeaponType.Barehands) },
            { WeaponType.Knife, new Weapon("w2", "Knife", WeaponType.Knife) },
            { WeaponType.Pistol, null },
            { WeaponType.Rifle, null }
        };
        currentEquipped = WeaponType.Barehands;

        //spawning all prefabs
        foreach (var pair in weaponSlotConfigs)
        {
            WeaponType slot = pair.Key;
            WeaponSlotConfig config = pair.Value;
            if (config.weaponPrefab != null)
            {
                spawnedWeapons[slot] = Instantiate(config.weaponPrefab, config.handTransform);
                spawnedWeapons[slot].SetActive(false);
            }
        }

    }

    public bool Equip(WeaponType weaponType)
    {
        if (currentEquipped == weaponType)
        {
            Debug.Log("Already equipped so unequipping: " + weaponType);
            Unequip();
            return false;
        }
        else
        {
            if (!slots.TryGetValue(weaponType, out Weapon wep))
            {
                Debug.Log("1");
            }
            if (!slots.TryGetValue(weaponType, out Weapon weapon) || weapon == null)
            {
                Debug.Log("Weapon not found in slots : " + weaponType);
                return false;
            }

            if (spawnedWeapons.TryGetValue(currentEquipped, out GameObject oldWeaponPrefab) && oldWeaponPrefab != null)
            {
                oldWeaponPrefab.SetActive(false);
            }

            if (spawnedWeapons.TryGetValue(weaponType, out GameObject newWeaponPrefab) && newWeaponPrefab != null)
            {
                Debug.Log("Equipping weapon: " + weaponType + (int)weaponType);
                //Animation
                playerAnimation.UpdateWeaponAnimation((int)weaponType);

                //Logic
                newWeaponPrefab.SetActive(true);
                newWeaponPrefab.transform.SetPositionAndRotation(weaponSlotConfigs[weaponType].handTransform.position, player.location.rotation);
                currentEquipped = weaponType;

                //Listeners
                OnWeaponEquipped?.Invoke(newWeaponPrefab.GetComponent<IWeaponBehaviour>());
                return true;
            }
            else
            {
                Debug.LogError("Weapon prefab not found for the specified weapon type");
                return false;
            }

        }

    }

    public void Unequip()
    {
        Equip(WeaponType.Barehands);
    }

    public WeaponType GetCurrentEquipped()
    {
        return currentEquipped;
    }

    #region EVENTS
    public event Action<IWeaponBehaviour> OnWeaponEquipped;
    #endregion




    // private GameObject currentWeapon;
    // private Dictionary<int, Transform> weaponTransforms;
    // private bool[] isWeaponEquipped;


    // public GameObject[] weapons;


    // private int currentWeaponIndex;
    // private int totalWeapons;
    // private PlayerMotor playerMotor;


    // public void Start()
    // {
    //     playerMotor = GetComponentInParent<PlayerMotor>();

    //     totalWeapons = 4;

    //     currentWeaponIndex = -1;
    //     isWeaponEquipped = new bool[totalWeapons];

    //     // Initialize the weapon transforms
    //     weaponTransforms = new Dictionary<int, Transform>();
    //     if (bareHandTransform) weaponTransforms.Add(0, bareHandTransform);
    //     if (swordHandTransform) weaponTransforms.Add(1, swordHandTransform);
    //     if (pistolHandTransform) weaponTransforms.Add(2, pistolHandTransform);
    //     if (rifleHandTransform) weaponTransforms.Add(3, rifleHandTransform);

    //     // Initialize the weapons array
    //     weapons = new GameObject[totalWeapons];



    //     for (int i = 0; i < totalWeapons; i++)
    //     {
    //         if (weapons[i] == null)
    //         {
    //             Debug.LogError("Weapon prefab is not assigned in the inspector for index: " + i);
    //             continue;
    //         }

    //         Transform handTransform = weaponTransforms[i];
    //         isWeaponEquipped[i] = false;
    //         weapons[i] = Instantiate(weapons[i], handTransform, worldPositionStays: false);
    //         weapons[i].transform.rotation = playerMotor.GetCurrentFacingDirection().rotation;
    //         Vector3 pos = weapons[i].transform.position;
    //         pos.x += 0.008f;
    //         pos.y += 0.0212f;
    //         weapons[i].transform.position = pos;
    //         weapons[i].SetActive(false);
    //     }
    // }


    // public int IsWeaponEquipped()
    // {
    //     for (int i = 0; i < totalWeapons; i++)
    //     {
    //         if (isWeaponEquipped[i])
    //             return i;
    //     }
    //     return -1;
    // }

    //     void Update()
    //     {
    //         if (Input.GetKeyDown(KeyCode.Alpha1)) HandleEquipUnequip(0);
    //         else if (Input.GetKeyDown(KeyCode.Alpha2)) HandleEquipUnequip(1);
    //         else if (Input.GetKeyDown(KeyCode.Alpha3)) HandleEquipUnequip(2);

    //         if (Input.GetKeyDown(KeyCode.F))
    //         {
    //             Debug.Log("Current Weapon Index : " + currentWeaponIndex);
    //             Debug.Log("Current Weapons : " + string.Join(", ", weapons.Select(w => w.name).ToArray()));
    //         }
    //     }

    //     private void HandleEquipUnequip(int i)
    //     {
    //         if (currentWeaponIndex != -1 && currentWeaponIndex != i)
    //         {
    //             Debug.Log("Unequipping weapon non animated" + currentWeaponIndex);
    //             UnequipWeapon(currentWeaponIndex, false);
    //             isWeaponEquipped[currentWeaponIndex] = false;
    //         }

    //         if (i >= totalWeapons)
    //         {
    //             Debug.Log("Nothing to equip/unequip");
    //             return;
    //         }

    //         if (!isWeaponEquipped[i])
    //         {
    //             Debug.Log("Equipping weapon " + i);
    //             EquipWeapon(i);
    //             isWeaponEquipped[i] = true;
    //         }
    //         else
    //         {
    //             Debug.Log("Unequipping weapon " + i);
    //             UnequipWeapon(i);
    //             isWeaponEquipped[i] = false;
    //         }
    //     }

    //     private void EquipWeapon(int i)
    //     {
    //         if (i == currentWeaponIndex)
    //         {
    //             Debug.Log("Already equipped");
    //             return;
    //         }

    //         if (i >= totalWeapons)
    //         {
    //             Debug.Log("Nothing to equip");
    //             return;
    //         }

    //         playerAnimation.UpdateWeaponAnimation(i);
    //         // StartCoroutine(DelayedEquipWeapon(i));

    //         currentWeapon = weapons[i];
    //         currentWeapon.SetActive(true);
    //         currentWeapon.transform.SetPositionAndRotation(handTransform.position, playerMotor.GetCurrentFacingDirection().rotation);
    //         currentWeaponIndex = i;
    //     }

    //     private void UnequipWeapon(int i, bool anim = true)
    //     {
    //         if (i >= totalWeapons || i < 0)
    //         {
    //             Debug.Log("Nothing to unequip");
    //             return;
    //         }

    //         if (anim)
    //         {
    //             playerAnimation.UpdateWeaponAnimation(-1);
    //         }

    //         if (currentWeapon != null)
    //         {
    //             currentWeapon.SetActive(false);
    //             currentWeapon = null;
    //             currentWeaponIndex = -1;
    //         }
    //     }
    // }
}
