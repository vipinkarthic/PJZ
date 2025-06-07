using System;

// Weapon.cs
[System.Serializable]
public class Weapon
{
    public string id;
    public string name;
    public WeaponType slot;

    public Weapon(string _id, string _name, WeaponType _slot)
    {
        id = _id;
        name = _name;
        slot = _slot;
    }
}
