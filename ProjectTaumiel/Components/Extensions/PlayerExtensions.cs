using CommandSystem;
using Exiled.API.Features;
using ProjectTaumiel.Components.Features;

namespace ProjectTaumiel.Components.Extensions;

public static class PlayerExtensions
{
    public static Player AsPlayer(this ICommandSender sender)
        => Player.Get(sender);

    public static ProjectTaumielProperties ProjectTaumiel(this Player player)
        => player.ReferenceHub.GetComponent<ProjectTaumielProperties>();
}