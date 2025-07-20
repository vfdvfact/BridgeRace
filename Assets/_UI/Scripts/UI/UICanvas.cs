﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    //public bool IsAvoidBackKey = false;

    protected RectTransform m_RectTransform;


    private void Start()
    {
        Init();
    }

    protected void Init()
    {
        m_RectTransform = GetComponent<RectTransform>();

        //float ratio = (float)Screen.height / (float)Screen.width;

        //// xu ly tai tho
        //if (ratio > 2.1f)
        //{
        //    Vector2 leftBottom = m_RectTransform.offsetMin;
        //    Vector2 rightTop = m_RectTransform.offsetMax;
        //    rightTop.y = -100f;
        //    m_RectTransform.offsetMax = rightTop;
        //    leftBottom.y = 0f;
        //    m_RectTransform.offsetMin = leftBottom;
        //    m_OffsetY = 100f;
        //}
        //m_IsInit = true;
    }

    public virtual void Setup()
    {
        UIManager.Instance.AddBackUI(this);
        UIManager.Instance.PushBackAction(this, BackKey);
    }

    public virtual void BackKey()
    {

    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        //anim
    }

    public virtual void Close()
    {
        UIManager.Instance.RemoveBackUI(this);
        //anim
        gameObject.SetActive(false);
        
    }


}
