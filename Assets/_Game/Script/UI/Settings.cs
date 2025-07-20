using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : UICanvas
{
    public override void Open()
    {
        GameManager.Instance.ChangeState(GameState.Pause);
        base.Open();
    }
    public void ContinueButton()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
        LevelManager.Instance.OnContinue();
        UIManager.Instance.OpenUI<Gameplay>();
        Close();
    }

    public void RetryButton()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
        LevelManager.Instance.OnRetry();
        Close();
    }
}
