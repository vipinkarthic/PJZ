using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour, IDamageable
{
    [SerializeField] private Canvas playerStatsCanvas;
    private readonly List<PlayerStat> stats = new();

    //sliders :
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider strengthSlider;
    [SerializeField] private Slider agilitySlider;
    [SerializeField] private Slider resistanceSlider;

    // Helpers
    public float GetHealthStat()
    {
        return stats[0].currentValue;
    }

    public float GetStrengthStat()
    {
        return stats[1].currentValue;
    }

    public float GetAgilityStat()
    {
        return stats[2].currentValue;
    }

    public float GetResistanceStat()
    {
        return stats[3].currentValue;
    }

    private void Awake()
    {
        // Health stat : 
        PlayerStat healthStat = new();
        healthStat.Initialize(100f);

        // Strength stat :
        PlayerStat strengthStat = new();
        strengthStat.Initialize(10f);

        // Agility stat :
        PlayerStat agilityStat = new();
        agilityStat.Initialize(5f);

        // Resistance stat :
        PlayerStat resistanceStat = new();
        resistanceStat.Initialize(3f);

        // Add stats to the list
        stats.Add(healthStat);
        stats.Add(strengthStat);
        stats.Add(agilityStat);
        stats.Add(resistanceStat);
    }

    private void Start()
    {
        // Initialize the sliders
        healthSlider.maxValue = stats[0].maxValue;
        healthSlider.value = stats[0].currentValue;

        strengthSlider.maxValue = stats[1].maxValue;
        strengthSlider.value = stats[1].currentValue;

        agilitySlider.maxValue = stats[2].maxValue;
        agilitySlider.value = stats[2].currentValue;

        resistanceSlider.maxValue = stats[3].maxValue;
        resistanceSlider.value = stats[3].currentValue;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void ResetAllStats()
    {
        foreach (PlayerStat stat in stats)
        {
            stat.ResetStats();
        }
    }

    #region Health 
    public void TakeDamage(float damage = 10f)
    {
        // it will depend on the resistance stat but for now this is the place holder
        PlayerStat healthStat = stats[0];
        healthStat.SubtractValue(damage);

        // Update the health slider
        healthSlider.value = healthStat.currentValue;
    }
    #endregion
}
