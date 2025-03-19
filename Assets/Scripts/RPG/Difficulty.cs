using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    enum DifficultySettings
    {
        Hard,
        Normal,
        Easy
    }
    [SerializeField] DifficultySettings difficultySettings = DifficultySettings.Hard;
    
    // Adult Typing Speed / 17+ years old
    [SerializeField]
    enum HardDifficulty
    {
        Master = 350,
        Perfect = 300,
        Average = 250,
        Begginer = 200
    }
    // 12 to 16 years old Typing Speed
    [SerializeField]
    enum NormalDifficulty
    {
        Master = 300,
        Perfect = 250,
        Average = 200,
        Begginer = 150
    }
    // 6 to 11 years old Typing Speed
    [SerializeField]
    enum EasyDifficulty
    {
        Master = 225,
        Perfect = 175,
        Average = 125,
        Begginer = 75
    }

    //Sets Player Action Effectiveness variable and constraints in applying values
    public int actionEffectiveness
    {
        get 
        { 
            return actionEffectiveness;  
        }

        private set
        {
            if(value > 6)
            {
                actionEffectiveness = 6;
            }
            else if(value < 1)
            {
                actionEffectiveness = 1;
            }
            else
            {
                actionEffectiveness = value;
            }
        }
    }

    
    void GetActionEffectiveness()
    {
        switch (difficultySettings)
        {
            case DifficultySettings.Hard:
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
            case DifficultySettings.Normal:
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
            case DifficultySettings.Easy:
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
    }

    //Old ActionEffectiveness Variable version

    /*1 effectiveness is pity, 2 is beginner, 3 is average,
    4 is great/perfect, 5 is master(semi-impossible)
    [Range(1, 6)] int actionEffectiveness;*/


    /*  Original Intention of repeating code in all switch cases for
        Determining Action Effectiveness
     
        //Master / Bot
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
