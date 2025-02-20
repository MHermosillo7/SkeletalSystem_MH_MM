using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class Enemy : MonoBehaviour
{
    [SerializeField] int minDamage;
    [SerializeField] int maxDamage;
    int currentDamage;

    [SerializeField] Image actionUI;

    GameObject player;
    Health playerHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    enum ActionStatus
    {
        attack,
        defend,
        wait
    }

    public void GetAttack()
    {
        //For some reason, when using integers in Random.Range,
        //the highest number is exclusive. ie, it would have 0% chance to
        //hit for max damage unless one is added to highest number so as to "include it"
        currentDamage = Random.Range(minDamage, maxDamage + 1);

        //actionUI
    }

    public void Attack()
    {
        
    }
}
