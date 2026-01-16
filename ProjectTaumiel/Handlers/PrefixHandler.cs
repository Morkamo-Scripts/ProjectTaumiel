using System.Collections;
using Exiled.API.Features;
using MEC;
using MorkamoEventsRegistrator.Components;
using ProjectTaumiel.Components.Features;
using ProjectTaumiel.Events;
using ProjectTaumiel.Events.EventArgs.Player;
using UnityEngine;

namespace ProjectTaumiel.Handlers;

public class PrefixHandler : IEventsRegistrator
{
    public void RegisterEvents()
    {
        EventManager.PlayerEvents.PlayerFullConnected += OnPlayerFullConnected;
    }

    public void UnregisterEvents()
    {
        EventManager.PlayerEvents.PlayerFullConnected -= OnPlayerFullConnected;
    }

    private void OnPlayerFullConnected(PlayerFullConnectedEventArgs ev) =>
        CoroutineRunner.Run(PrefixHandlerCoroutine(ev.Player));

    private IEnumerator PrefixHandlerCoroutine(Player player)
    {
        while (player != null)
        {
            yield return new WaitForSeconds(0.2f);
            player.RankName = "★ Taumiel ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.2f);
            player.RankName = "★ TAumiel ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.2f);
            player.RankName = "★ TAUmiel ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.2f);
            player.RankName = "★ TAUMiel ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.2f);
            player.RankName = "★ TAUMIel ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.2f);
            player.RankName = "★ TAUMIEl ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.2f);
            player.RankName = "★ TAUMIEL ★";
            player.RankColor = "pink";
            
            yield return new WaitForSeconds(0.2f);
            player.RankName = "★ tAUMIEL ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.2f);
            player.RankName = "★ taUMIEL ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.2f);
            player.RankName = "★ tauMIEL ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.2f);
            player.RankName = "★ taumIEL ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.2f);
            player.RankName = "★ taumiEL ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.2f);
            player.RankName = "★ taumieL ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.2f);
            player.RankName = "★ taumiel ★";
            player.RankColor = "pink";
            
            yield return new WaitForSeconds(0.2f);
            
            player.RankName = "★ Taumiel ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.5f);
            player.RankName = "☆ Taumiel ☆";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.5f);
            player.RankName = "★ Taumiel ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.5f);
            player.RankName = "☆ Taumiel ☆";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.5f);
            player.RankName = "★ Taumiel ★";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.5f);
            player.RankName = "☆ Taumiel ☆";
            player.RankColor = "pink";
            yield return new WaitForSeconds(0.5f);
            player.RankName = "★ Taumiel ★";
            player.RankColor = "pink";
        }
    }
}