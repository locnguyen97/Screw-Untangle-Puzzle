using System.Collections;
using System.Collections.Generic;
using com.adjust.sdk;
using UnityEngine;

public class AdjustManager : MonoBehaviour
{
    public IdConfig config;
    // Start is called before the first frame update
    public void Awake()
    {  
        var adjustConfig = new AdjustConfig(config.adjustId, AdjustEnvironment.Production, true);
        Debug.LogWarning("Adjust ID:" + config.adjustId);
        adjustConfig.setLogLevel(AdjustLogLevel.Suppress);
        adjustConfig.setSendInBackground(false);
        adjustConfig.setEventBufferingEnabled(false);
        adjustConfig.setLaunchDeferredDeeplink(false);
        Adjust.start(adjustConfig);
    }
}
