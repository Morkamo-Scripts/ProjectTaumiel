using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Items.Usables;
using ProjectTaumiel.Components.Features;
using ProjectTaumiel.Events;
using ProjectTaumiel.Handlers;
using events = Exiled.Events.Handlers;

namespace ProjectTaumiel
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "ProjectTaumiel";
        public override string Prefix => Name;
        public override string Author => "Morkamo";
        public override Version Version => new(1, 0, 0);
        public override Version RequiredExiledVersion => new(9, 12, 5);
        
        public static Plugin Instance { get; private set; }
        public PlayerTHandler PlayerTHandler { get; private set; }
        public PrefixHandler PrefixHandler { get; private set; }
        
        public override void OnEnabled()
        {
            Instance = this;
            events.Player.Verified += OnVerifiedPlayer;
            
            PlayerTHandler = new PlayerTHandler();
            PrefixHandler = new PrefixHandler();
            
            MorkamoEventsRegistrator.Plugin.AddRegistrator(PlayerTHandler);
            MorkamoEventsRegistrator.Plugin.AddRegistrator(PrefixHandler);
            
            DatabaseHandler.InitializeDatabase();
            base.OnEnabled();
        }
        
        public override void OnDisabled()
        {
            DatabaseHandler.Shutdown();
            
            MorkamoEventsRegistrator.Plugin.RemoveRegistrator(PlayerTHandler);
            MorkamoEventsRegistrator.Plugin.RemoveRegistrator(PrefixHandler);
            
            PlayerTHandler = null;
            PrefixHandler = null;
            
            events.Player.Verified -= OnVerifiedPlayer;
            Instance = null;
            base.OnDisabled();
        }
        
        private void OnVerifiedPlayer(VerifiedEventArgs ev)
        {
            if (ev.Player == null || ev.Player.IsNPC)
                return;
            
            if (ev.Player.ReferenceHub.gameObject.GetComponent<ProjectTaumielProperties>() != null)
                return;

            ev.Player.ReferenceHub.gameObject.AddComponent<ProjectTaumielProperties>();
            EventManager.PlayerEvents.InvokePlayerFullConnected(ev.Player);
        }
    }
}