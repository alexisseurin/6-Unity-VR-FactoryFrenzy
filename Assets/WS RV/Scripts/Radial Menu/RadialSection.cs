using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class RadialSection
{
    public string text;
    public TMP_Text content;
    public UnityEvent onSelect = new UnityEvent();
}
