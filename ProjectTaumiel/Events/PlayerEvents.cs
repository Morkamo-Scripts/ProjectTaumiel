using System;
using Exiled.API.Features;
using ProjectTaumiel.Events.EventArgs.Player;

namespace ProjectTaumiel.Events
{
    public partial class PlayerEvents
    {
        public event Action<PlayerFullConnectedEventArgs> PlayerFullConnected;
    }

    public partial class PlayerEvents
    {
        public void InvokePlayerFullConnected(Player player)
        {
            var ev = new PlayerFullConnectedEventArgs(player);
            PlayerFullConnected?.Invoke(ev);
        }
    }
}