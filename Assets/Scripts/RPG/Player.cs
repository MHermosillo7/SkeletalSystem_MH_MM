using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Image actionUI;

    GameObject selectedEnemy;

    //1 effectiveness is pity, 2 is beginner, 3 is average,
    //4 is great/perfect, 5 is master (semi-impossible)
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
    [SerializeField] Difficulty difficultySettings = Difficulty.Hard;

    // Adult Typing Speed / 17+ years old
    [SerializeField] enum HardDifficulty
    {
        Master = 350,
        Perfect = 300,
        Average = 250,
        Begginer = 200
    }
    // 12 to 16 years old Typing Speed
    [SerializeField] enum NormalDifficulty
    {
        Master = 300,
        Perfect = 250,
        Average = 200,
        Begginer = 150
    }
    // 6 to 11 years old Typing Speed
    [SerializeField] enum EasyDifficulty
    {
        Master = 225,
        Perfect = 175,
        Average = 125,
        Begginer = 75
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
        switch (difficultySettings)
        {
            case Difficulty.Hard:
                //Master / Bot
                if (charactersTyped >= (int)HardDifficulty.Master)
                {
                    actionEffectiveness = 5;
                }
                //Above average / Expert
                else if (charactersTyped >= (int)HardDifficulty.Perfect)
                {
                    actionEffectiveness = 4;
                }
                //Average
                else if (charactersTyped >= (int)HardDifficulty.Average)
                {
                    actionEffectiveness = 3;
                }
                //Below average
                else if (charactersTyped >= (int)HardDifficulty.Begginer)
                {
                    actionEffectiveness = 2;
                }
                //Pity
                else
                {
                    actionEffectiveness = 1;
                }
                break;
            case Difficulty.Normal:
                //Master / Bot
                if (charactersTyped >= (int)HardDifficulty.Master)
                {
                    actionEffectiveness = 5;
                }
                //Above average / Expert
                else if (charactersTyped >= (int)HardDifficulty.Perfect)
                {
                    actionEffectiveness = 4;
                }
                //Average
                else if (charactersTyped >= (int)HardDifficulty.Average)
                {
                    actionEffectiveness = 3;
                }
                //Below average
                else if (charactersTyped >= (int)HardDifficulty.Begginer)
                {
                    actionEffectiveness = 2;
                }
                //Pity
                else
                {
                    actionEffectiveness = 1;
                }
                break;
            case Difficulty.Easy:
                //Master / Bot
                if (charactersTyped >= (int)EasyDifficulty.Master)
                {
                    actionEffectiveness = 5;
                }
                //Above average / Expert
                else if (charactersTyped >= (int)EasyDifficulty.Perfect)
                {
                    actionEffectiveness = 4;
                }
                //Average
                else if (charactersTyped >= (int)EasyDifficulty.Average)
                {
                    actionEffectiveness = 3;
                }
                //Below average
                else if (charactersTyped >= (int)EasyDifficulty.Begginer)
                {
                    actionEffectiveness = 2;
                }
                //Pity
                else
                {
                    actionEffectiveness = 1;
                }
                break;
            default:
                //Master / Bot
                if (charactersTyped >= (int)NormalDifficulty.Master)
                {
                    actionEffectiveness = 5;
                }
                //Above average / Expert
                else if (charactersTyped >= (int)NormalDifficulty.Perfect)
                {
                    actionEffectiveness = 4;
                }
                //Average
                else if (charactersTyped >= (int)NormalDifficulty.Average)
                {
                    actionEffectiveness = 3;
                }
                //Below average
                else if (charactersTyped >= (int)NormalDifficulty.Begginer)
                {
                    actionEffectiveness = 2;
                }
                //Pity
                else
                {
                    actionEffectiveness = 1;
                }
                break;
        }

        //Original Intention of repeating code in all switch cases
        /*//Master / Bot
        if (charactersTyped >= difficulty.Master.ConvertTo<Int32>())
        {
            actionEffectiveness = 6;
        }
        //Expert
        else if (charactersTyped >= difficulty.Perfect)
        {
            actionEffectiveness = 5;
        }
        //Above average
        else if (charactersTyped >= difficulty.Average)
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
        }*/
    }

    //Key management down to end
    private static readonly KeyCode[] keyCodes = Enum.GetValues(typeof(KeyCode))
            .Cast<KeyCode>()
            .Where(k => ((int)k < (int)KeyCode.Mouse0) && ((int)k > 0))
            .ToArray();

    private void Start()
    {
        GetActionEffectiveness();
    }
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

        if(pressedKey == "None")
        {
            return;
        }


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
