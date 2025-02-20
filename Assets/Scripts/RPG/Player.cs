using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Image actionUI;

    GameObject selectedEnemy;

    //One effectiveness is pity, 2 is average, 3 is great/perfect, 4 is master (semi-impossible)
    [Range(1, 6)] int actionEffectiveness;

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

    enum Difficulty
    {
        Hard,
        Normal,
        Easy
    }


    void Attack()
    {
        selectedEnemy.GetComponent<Health>().ChangeHealth(physicalDamage, "Physical");
    }
    void SpellCast()
    {
        selectedEnemy.GetComponent<Health>().ChangeHealth(spellDamage, "Spell");
    }
    void Defend()
    {
        GetComponent<Health>().ChangeDefense(actionEffectiveness);
    }

    void GetActionEffectiveness()
    {
        //Master / Bot
        if (charactersTyped >= 350)
        {
            actionEffectiveness = 6;
        }
        //Expert
        else if (charactersTyped >= 300)
        {
            actionEffectiveness = 5;
        }
        //Above average
        else if (charactersTyped >= 250)
        {
            actionEffectiveness = 4;
        }
        //Average
        else if (charactersTyped >= 200)
        {
            actionEffectiveness = 3;
        }
        //Below average
        else if (charactersTyped >= 150)
        {
            actionEffectiveness = 2;
        }
        //Pity
        else
        {
            actionEffectiveness = 1;
        }
    }


    Dictionary<string, int> typingCategories = new Dictionary<string, int>();
        [SerializeField] Difficulty difficulty = Difficulty.Hard;
    void GetDifficulty()
    {
        typingCategories.Clear();

        switch (difficulty)
        {
            case Difficulty.Hard:

                break;
            case Difficulty.Normal:
                break;
            case Difficulty.Easy:
                break;
        }
    }

    //Key management down to end
    private static readonly KeyCode[] keyCodes = Enum.GetValues(typeof(KeyCode))
            .Cast<KeyCode>()
            .Where(k => ((int)k < (int)KeyCode.Mouse0) && ((int)k > 0))
            .ToArray();
           
    void Update()
    {
        print(GetPressedKey().ToString());
    }
    private static KeyCode GetCurrentKeyDown()
    {
        return KeyCode.Escape;
    }

    void GetInput()
    {
        string pressedKey = GetPressedKey().ToString();


    }

    public KeyCode GetPressedKey()
    {
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    return keyCodes[i];
                }
            }
        }

        return KeyCode.None;
    }
    public static IEnumerable<KeyCode> GetPressedKeysMultiple()
    {
        
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                {
                    yield return keyCodes[i];
                }
            }
        }
    }
    //Way to only get letter keys
    /*private static readonly KeyCode[] keyCodes = Enum.GetValues(typeof(KeyCode))
            .Cast<KeyCode>()
            .Where(k => ((int)k <= 122) && ((int)k >= 97))
            .ToArray();*/
}
