using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtscore;

    [SerializeField]
    private Text txtInfo;

    [SerializeField]
    private CanvasGroup canvasGroupInfo;

    public void UpdateDisplayScore(int score)
    {
        txtscore.text = score.ToString();
    }

    public void DisplayGameOverInfo()
    {
        canvasGroupInfo.DOFade(1.0f, 1.0f);
        txtInfo.DOText("Game Over...", 1.0f);
    }
}
