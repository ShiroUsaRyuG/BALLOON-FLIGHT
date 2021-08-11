using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtscore;

    public void UpdateDisplayScore(int score)
    {
        txtscore.text = score.ToString();
    }
}
