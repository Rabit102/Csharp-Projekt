using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [System.Serializable]
    public class Upgrade
    {
        public string id;
        public int maxLevel;
        public int currentLevel;
        public int baseCost;

        public int GetCost() => baseCost * (currentLevel + 1);
    }

public static UpgradeManager Instance;
public Upgrade[] upgrades;
public TMP_Text[] upgradeTexts;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void TryBuyUpgrade(int index)
    {
        Upgrade target = upgrades[index];
        int cost = target.GetCost();

        if(target.currentLevel >= target.maxLevel) return;
        if(GameManager.Instance.score < cost) return;

        GameManager.Instance.AddScore(-cost);
        target.currentLevel++;
        ApplyUpgradeEffect(target.id);
        UpdateUI();
    }

    public void ApplyUpgradeEffect(string id)
    {
        switch(id)
        {
            case "tower_health":
            GameManager.Instance.tower.IncreaseMaxHealth(1);
            break;

            case "bullet_slow":
            BulletSpawner.Instance.bulletSpeedMultiplier *= 0.85f;
            break;

            case "score_bonus":
            GameManager.Instance.scoreMultiplier *= 1.25f;
            break;

            case "energy_shield":
            GameManager.Instance.tower.AddShield();
            break;
        }
    }

    public void UpdateUI()
{
    for (int i = 0; i < upgrades.Length; i++)
    {
        string currentText = upgradeTexts[i].text;
        string upgradeName = currentText.Split('\n')[0];

        upgradeTexts[i].text = 
            $"{upgradeName}\n" +
            $"Level: {upgrades[i].currentLevel}/{upgrades[i].maxLevel}\n" +
            $"Cost: {upgrades[i].GetCost()}";
    }
}
}