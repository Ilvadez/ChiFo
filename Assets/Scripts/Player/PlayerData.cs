using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public SpeedData SpeedData;
}

[Serializable]
public struct SpeedData
{
    public float Speed;
    public float Acceleration;
    public float Friction;
} 
