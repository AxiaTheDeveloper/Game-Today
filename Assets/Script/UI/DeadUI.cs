using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DeadUI : MonoBehaviour
{
    [SerializeField]private CanvasGroup BG_DeadUI, DeadUIScore;
    [SerializeField]private TextMeshProUGUI timerText, enemyDiedText;
    private void Awake() {
        DeadUIScore.alpha = 0;
    }
    private void UpdateData()
    {
        var ts = TimeSpan.FromSeconds(TimerManager.Instance.GetTime());
        timerText.text = string.Format("{0:00}:{1:00}", ts.TotalMinutes, ts.Seconds);
        enemyDiedText.text = PlayerIdentity.Instance.GetEnemyTotalDead().ToString();
    }
    public void HideDeadUI()
    {
        BG_DeadUI.LeanAlpha(0f, 0.5f).setOnComplete(
            () => StahlGameManager.Instance.StartGameCourotine()
        );
    }
    public void ShowDeadUI()
    {
        UpdateData();
        BG_DeadUI.LeanAlpha(1f, 0.3f).setOnComplete(
            () => DeadScore()
        );
    }
    public void DeadScore()
    {
        DeadUIScore.LeanAlpha(1f, 0.3f);
    }
    public void HideDeadScore()
    {
        DeadUIScore.LeanAlpha(1f, 0.5f);
    }


}
