using System.Collections;
using System.Collections.Generic;
using Shapes;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class DotOrderSwitch : MonoBehaviour
{
    public Line A;
    public Line B;
    public DotProductVisualiser Visualiser;
    public TMP_Text buttonText;

    public void SwapOrder()
    {
        Visualiser.lineA = Visualiser.lineA == A ? B : A;
        Visualiser.lineB = Visualiser.lineB == B ? A : B;
        
        string aText = $"<color=#{A.Color.ToHexString()}>A</color>";
        string bText = $"<color=#{B.Color.ToHexString()}>B</color >";

        buttonText.text = Visualiser.lineA == A ? $"{aText} \u2219 {bText}" : $"{bText} \u2219 {aText}";

    }
}
