using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] GameObject buffEfx;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay) )
        {
            if (Input.GetMouseButton(0))
            {

                Vector3 nextPoint = JoystickControl.direct * speed * Time.deltaTime + TF.position;

                if (CanMove(nextPoint))
                {
                    TF.position = CheckGround(nextPoint);
                }

                if (JoystickControl.direct != Vector3.zero)
                {
                    skin.forward = JoystickControl.direct;
                }

                ChangeAnim(Constants.ANIM_RUN);
            }

            if (Input.GetMouseButtonUp(0))
            {
                ChangeAnim(Constants.ANIM_IDLE);
            }

        }
    }
    public override void AddBrick()
    {
        base.AddBrick();
        if (!buffEfx.activeSelf && playerBricks.Count >= 4)
        {
            buffEfx.SetActive(true);
            speed *= 2;
        }
    }
    public override void RemoveBrick()
    {
        base.RemoveBrick();
        if (buffEfx.activeSelf && playerBricks.Count < 4)
        {
            buffEfx.SetActive(false);
            speed /= 2;
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        buffEfx.SetActive(false);
        speed = 5;
    }
}
