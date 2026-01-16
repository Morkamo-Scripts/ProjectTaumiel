using System;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using ProjectTaumiel.Components.Features;
using ProjectTaumiel.Components.Features.Components;
using events = Exiled.Events.Handlers;

namespace ProjectTaumiel
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "ProjectTaumiel";
        public override string Prefix => Name;
        public override string Author => "Morkamo";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 12, 5);
        
        public static Plugin Instance { get; private set; }
        
        public override void OnEnabled()
        {
            Instance = this;
            events.Player.Verified += OnVerifiedPlayer;
            base.OnEnabled();
        }
        
        public override void OnDisabled()
        {
            events.Player.Verified -= OnVerifiedPlayer;
            Instance = null;
            base.OnDisabled();
        }
        
        private void OnVerifiedPlayer(VerifiedEventArgs ev)
        {
            if (ev.Player.ReferenceHub.gameObject.GetComponent<ProjectTaumielProperties>() != null)
                return;

            ev.Player.ReferenceHub.gameObject.AddComponent<ProjectTaumielProperties>();
        }
    }
}