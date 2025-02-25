using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypingUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    Typing typing;

    [SerializeField] Color fadedColor;

    int letterIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Typing typing = FindObjectOfType<Typing>();

        text.text = typing.information;
        
    }
    void ChangeColor()
    {
        for (int i = 0; i < text.textInfo.characterCount; i++)
        {
            //An attempt was made...
            //text.textInfo.characterInfo[i].color = Random.ColorHSV();

            Color32 color = Random.ColorHSV();

            int meshIndex = text.textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = text.textInfo.characterInfo[i].vertexIndex;

            Color32[] vertexColors = text.textInfo.meshInfo[meshIndex].colors32;

            vertexColors[vertexIndex + 0] = color;
            vertexColors[vertexIndex + 1] = color;
            vertexColors[vertexIndex + 2] = color;
            vertexColors[vertexIndex + 3] = color;
        }
        text.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }
    public void FadeLetter()
    {
        int meshIndex = text.textInfo.characterInfo[letterIndex].materialReferenceIndex;
        int vertexIndex = text.textInfo.characterInfo[letterIndex].vertexIndex;

        Color32[] vertexColors = text.textInfo.meshInfo[meshIndex].colors32;

        vertexColors[vertexIndex + 0] = fadedColor;
        vertexColors[vertexIndex + 1] = fadedColor;
        vertexColors[vertexIndex + 2] = fadedColor;
        vertexColors[vertexIndex + 3] = fadedColor;

        letterIndex++;

        text.transform.position =
            new Vector2(text.transform.position.x - 15, text.transform.position.y);
    }
    string ColorToHex(Color color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");

        return hex;
    }
}
