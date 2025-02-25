using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class Health : ScriptableObject
{
    [SerializeField] int maxHealth;
    int currentHealth;

    [SerializeField] int defense;

    enum Weakness
    {
        physical,
        spell
    }

    [SerializeField] Weakness weakness;

    [SerializeField] Image actionUI;

    [SerializeField] Text healthUI;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(int healthChange, string attackType)
    {
        currentHealth += healthChange;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthUI.text = currentHealth.ToString();
    }

    //Way to reset defense at the end of each turn
    public void ResetDefense()
    {
        defense = 0;
    }

    //Way to add or substract defense
    public void ChangeDefense(int defenseChange)
    {
        defense += defenseChange;
    }
}
