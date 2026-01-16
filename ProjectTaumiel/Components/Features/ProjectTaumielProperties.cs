using Exiled.API.Features;
using ProjectTaumiel.Components.Features.Components;
using UnityEngine;

namespace ProjectTaumiel.Components.Features;

public sealed class ProjectTaumielProperties() : MonoBehaviour
{
    private void Awake()
    {
        Player = Player.Get(gameObject);
        TaumielProperties = new TaumielProperties(this);
    }
    
    public Player Player { get; private set; }
    public TaumielProperties TaumielProperties { get; private set; }
}