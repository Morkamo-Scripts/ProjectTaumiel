using Exiled.API.Features;
using LabApi.Events;
using MEC;
using MorkamoEventsRegistrator.Components;
using ProjectTaumiel.Events.EventArgs.Player;
using UnityEngine;
using EventManager = ProjectTaumiel.Events.EventManager;

namespace ProjectTaumiel.Handlers;

public class PlayerTHandler : IEventsRegistrator
{
    public void RegisterEvents()
    {
        EventManager.PlayerEvents.PlayerFullConnected += OnPlayerFullConnected;
    }

    public void UnregisterEvents()
    {
        EventManager.PlayerEvents.PlayerFullConnected -= OnPlayerFullConnected;
    }

    private void OnPlayerFullConnected(PlayerFullConnectedEventArgs ev)
    {
        /*Timing.CallDelayed(2f, () =>
        {
            ev.Player.RankName = "★ Taumiel ★";
            ev.Player.RankColor = "silver";
            Log.Info("HE");
        });*/
    }
}