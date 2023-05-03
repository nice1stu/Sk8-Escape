using UnityEngine;
using UnityEditor;

public class LootBoxSimulator : MonoBehaviour
{
    private const float LootBoxCooldown = 10f;

    [ContextMenuItem("Open Loot Box 1", "OpenLootBox1")]
    public bool lootBox1Open = false;

    [ContextMenuItem("Open Loot Box 2", "OpenLootBox2")]
    public bool lootBox2Open = false;

    [ContextMenuItem("Open Loot Box 3", "OpenLootBox3")]
    public bool lootBox3Open = false;

    [ContextMenuItem("Open Loot Box 4", "OpenLootBox4")]
    public bool lootBox4Open = false;

    private float lootBox1CooldownEndTime;
    private float lootBox2CooldownEndTime;
    private float lootBox3CooldownEndTime;
    private float lootBox4CooldownEndTime;

    private void Update()
    {
        UpdateLootBoxCooldowns();
    }

    private void UpdateLootBoxCooldowns()
    {
        float currentTime = Time.time;

        if (lootBox1Open && currentTime >= lootBox1CooldownEndTime)
        {
            lootBox1Open = false;
            Debug.Log("Loot Box 1 opened!");
            lootBox1CooldownEndTime = currentTime + LootBoxCooldown;
        }

        if (lootBox2Open && currentTime >= lootBox2CooldownEndTime)
        {
            lootBox2Open = false;
            Debug.Log("Loot Box 2 opened!");
            lootBox2CooldownEndTime = currentTime + LootBoxCooldown;
        }

        if (lootBox3Open && currentTime >= lootBox3CooldownEndTime)
        {
            lootBox3Open = false;
            Debug.Log("Loot Box 3 opened!");
            lootBox3CooldownEndTime = currentTime + LootBoxCooldown;
        }

        if (lootBox4Open && currentTime >= lootBox4CooldownEndTime)
        {
            lootBox4Open = false;
            Debug.Log("Loot Box 4 opened!");
            lootBox4CooldownEndTime = currentTime + LootBoxCooldown;
        }
    }

    private void OpenLootBox1()
    {
        if (!lootBox1Open)
        {
            lootBox1Open = true;
            lootBox1CooldownEndTime = Time.time + LootBoxCooldown;
        }
    }

    private void OpenLootBox2()
    {
        if (!lootBox2Open)
        {
            lootBox2Open = true;
            lootBox2CooldownEndTime = Time.time + LootBoxCooldown;
        }
    }

    private void OpenLootBox3()
    {
        if (!lootBox3Open)
        {
            lootBox3Open = true;
            lootBox3CooldownEndTime = Time.time + LootBoxCooldown;
        }
    }

    private void OpenLootBox4()
    {
        if (!lootBox4Open)
        {
            lootBox4Open = true;
            lootBox4CooldownEndTime = Time.time + LootBoxCooldown;
        }
    }
}

