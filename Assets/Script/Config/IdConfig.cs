using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Config Id")]
public class IdConfig : ScriptableObject
{
    public string adjustId;
    public string emailSupport;
    public string projectChecklistUrl;
}
