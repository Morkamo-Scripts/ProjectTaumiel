using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using LabApi.Events;
using LabApi.Events.Arguments.PlayerEvents;
using MEC;
using MorkamoEventsRegistrator.Components;
using PlayerRoles;
using ProjectTaumiel.Components.Extensions;
using ProjectTaumiel.Components.Features;
using ProjectTaumiel.Events.EventArgs.Player;
using RueI.API;
using UnityEngine;
using EventManager = ProjectTaumiel.Events.EventManager;

namespace ProjectTaumiel.Handlers;

public class PlayerTHandler : IEventsRegistrator
{
    public void RegisterEvents()
    {
        EventManager.PlayerEvents.PlayerFullConnected += OnPlayerFullConnected;
        LabApi.Events.Handlers.PlayerEvents.Spawned +=  OnSpawned;
    }

    public void UnregisterEvents()
    {
        EventManager.PlayerEvents.PlayerFullConnected -= OnPlayerFullConnected;
        LabApi.Events.Handlers.PlayerEvents.Spawned -= OnSpawned;
    }

    private void OnPlayerFullConnected(PlayerFullConnectedEventArgs ev)
    {
        if (DatabaseHandler.IsPlayerExists(ev.Player.UserId))
            ev.Player.SetTaumielPlayer(true);
        
        if (!ev.Player.IsTaumielPlayer())
            return;

        ev.Player.ShowRueiHint(
            "<color=#523D2B>――――――――――――――――――――――――――――――――――――――</color>\n" +
            "<i><color=#F74888>Вы являетесь участником группы - </i><b><size=35><color=#999999>☆ Taumiel ☆</color></size></b>\n" +
            "</color><color=#EDA100><i>Для просмотра справки о роли и командах введите:</i></color> <size=35><color=#ED003B>tml help</color></size>\n" +
            "<color=#523D2B>――――――――――――――――――――――――――――――――――――――</color>",
            15f, 200);
    }

    private void OnSpawned(PlayerSpawnedEventArgs ev)
    {
        if (!ev.Player.AsExiled().IsTaumielPlayer())
            return;

        CoroutineRunner.Run(ShowAliveScp(ev.Player));
    }

    private IEnumerator ShowAliveScp(Player player)
    {
        while (player.IsConnected)
        {
            yield return new WaitForSeconds(1f);

            if (player.IsDead)
                continue;

            var aliveScpRoles = Player.List
                .Where(pl => pl.IsScp)
                .Select(pl => pl.Role.Type)
                .ToList();

            if (aliveScpRoles.Count == 0)
                continue;

            var parts = new List<string>();

            foreach (var group in aliveScpRoles.GroupBy(r => r))
            {
                if (group.Key == RoleTypeId.Scp0492)
                {
                    parts.Add($"SCP-049-2 ({group.Count()})");
                    continue;
                }

                var name = group.Key
                    .ToString()
                    .Replace("Scp", "SCP-");

                parts.Add(name);
            }

            var hintString = string.Join(" : ", parts);

            player.ShowRueiHint(
                $"<color=#CC8300><size=25><color=#7D7D7D>Живые SCP:</color> {hintString}</size></color>",
                1.2f,
                950f
            );
        }
    }
}