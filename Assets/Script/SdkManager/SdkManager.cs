using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SdkManager : MonoBehaviour
{
    public IdConfig config;
    public Reporter Reporter;
    
    public enum PositionActivate
    {
        TopLeft = 1 << 0,
        TopRight = 1 << 1,
        BotLeft = 1 << 2,
        BotRight = 1 << 3
    }
    
    public PositionActivate positionActivate = PositionActivate.TopRight;
    private int m_TestIndex;
    private void Awake()
    {
        Debug.unityLogger.filterLogType = LogType.Warning;
        Debug.LogWarning("Support Email:" + config.emailSupport);
    }

    private void Update()
    {
        if (!Input.GetMouseButtonUp(0)) return;

        var cornerSize = 0.2f;
        var isClickActivate = false;

        var isClickTop = Input.mousePosition.y > Screen.height * (1 - cornerSize);
        var isClickBot = Input.mousePosition.y < Screen.height * cornerSize;
        var isClickRight = Input.mousePosition.x > Screen.width * (1 - cornerSize);
        var isClickLeft = Input.mousePosition.x < Screen.width * cornerSize;

        isClickActivate |= ((positionActivate & PositionActivate.TopLeft) != 0 && isClickTop && isClickLeft);
        isClickActivate |= ((positionActivate & PositionActivate.TopRight) != 0 && isClickTop && isClickRight);
        isClickActivate |= ((positionActivate & PositionActivate.BotLeft) != 0 && isClickBot && isClickLeft);
        isClickActivate |= ((positionActivate & PositionActivate.BotRight) != 0 && isClickBot && isClickRight);

        if (isClickActivate)
        {
            m_TestIndex++;
            if (m_TestIndex == 8)
            {
                Reporter.doShow();
                m_TestIndex = 0;
            }
        }
        else
        {
            m_TestIndex = 0;
        }
    }
}
