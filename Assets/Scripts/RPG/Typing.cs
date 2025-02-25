using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Typing : MonoBehaviour
{
    int charactersTyped;

    KeyCode pressedKey;

    char nextCharacterToType;

    public string nextCharacter;

    public string information;

    List<char> charactersToType = new List<char>();

    //All keys in standard keybord
    private static readonly KeyCode[] keyCodes = Enum.GetValues(typeof(KeyCode))
            .Cast<KeyCode>()
            .Where(k => ((int)k < (int)KeyCode.Mouse0) && ((int)k > 0))
            .ToArray();

    TypingUI typingUI;

    private void Awake()
    {
        GetCharactersToType(information);

        typingUI = FindObjectOfType<TypingUI>();
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            TypingTest();
        }
    }

    private static KeyCode GetCurrentKeyDown()
    {
        return KeyCode.Escape;
    }

    void TypingTest()
    {
        //Input key filter
        GetInputKey();

        if (pressedKey == KeyCode.None)
        {
            return;
        }

        if (nextCharacterToType == '\0')
        {
            GetCharactersToType(information);

            nextCharacterToType = charactersToType[0];

            charactersToType.RemoveAt(0);
        }

        nextCharacter = nextCharacterToType.ToString();

        print(nextCharacter);

        if(nextCharacter != " " && pressedKey == 
            (KeyCode)Enum.Parse(typeof(KeyCode), 
            nextCharacter.ToUpper()))
        {
            GetNextCharacter();

            typingUI.FadeLetter();
        }
        else if(nextCharacter == " " && pressedKey ==
            (KeyCode)Enum.Parse(typeof(KeyCode),
            nextCharacter.ToUpper()))
        else
        {
            print("Try Again...");
        }
    }
    void GetInputKey()
    {
        KeyCode key = GetPressedKey();

        if (key == KeyCode.None)
        {
            pressedKey = KeyCode.None;
            return;
        }

        pressedKey = key;
    }

    void GetNextCharacter()
    {
        nextCharacterToType = charactersToType[0];

        charactersToType.RemoveAt(0);
    }

    void GetCharactersToType(string information)
    {
        charactersToType = information.ToCharArray().ToList();
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

    /*void GetInputKeys()
    {
        string key = GetPressedKeysMultiple().ToString();

        pressedKey = key;

        print(pressedKey);
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
    }*/

    //Way to only get letter keys
    /*private static readonly KeyCode[] keyCodes = Enum.GetValues(typeof(KeyCode))
            .Cast<KeyCode>()
            .Where(k => ((int)k <= 122) && ((int)k >= 97))
            .ToArray();*/

    /* char characterPressed;
     * 
     * void GetInputLetter(out char characterPressed)
    {
        string pressedKey = GetPressedKey().ToString();

        if (pressedKey == "None")
        {
            characterPressed = '\0';
            return;
        }

        characterPressed = pressedKey.ToCharArray()[0];
    }*/
}
