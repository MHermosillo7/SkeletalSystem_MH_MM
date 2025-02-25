using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Image actionUI;

    GameObject selectedEnemy;

    int charactersTyped;

    int physicalDamage;
    int spellDamage;

    enum ActionStatus
    {
        attacking,
        spellcasting,
        defending,
        healing,
        thinking
    }

    void Attack()
    {
        selectedEnemy.GetComponent<Health>().ChangeHealth(physicalDamage, "Physical");
    }
    void SpellCast()
    {
        selectedEnemy.GetComponent<Health>().ChangeHealth(spellDamage, "Spell");
    }
    //void Defend()
    //{
    //    GetComponent<Health>().ChangeDefense(actionEffectiveness);
    //}

    private void Start()
    {

    }
   
}
