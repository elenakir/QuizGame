using System;
using UnityEngine;

[Serializable]
public class CardData
{
    [SerializeField] private string _value;
    [SerializeField] private Sprite _icon;

    public string Value => _value;
    public Sprite Icon => _icon;
}
