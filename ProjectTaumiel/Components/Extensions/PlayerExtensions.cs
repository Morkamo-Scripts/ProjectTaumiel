using CommandSystem;
using Exiled.API.Features;
using JetBrains.Annotations;
using MEC;
using ProjectTaumiel.Components.Features;
using RueI.API;
using RueI.API.Elements;

namespace ProjectTaumiel.Components.Extensions;

public static class PlayerExtensions
{
    public static Player AsPlayer(this ICommandSender sender)
        => Player.Get(sender);
    
    public static Player AsExiled(this LabApi.Features.Wrappers.Player player)
        => Player.Get(player);

    public static ProjectTaumielProperties TaumielProperties(this Player player)
        => player.ReferenceHub.GetComponent<ProjectTaumielProperties>();
    
    public static bool IsTaumielPlayer(this Player player)
    {
        var props = player.TaumielProperties();
        return props != null && props.PlayerProperties.IsTaumielPlayer;
    }
    
    public static void SetTaumielPlayer(this Player player, bool value)
    {
        var props = player.TaumielProperties();
        if (props != null)
            props.PlayerProperties.IsTaumielPlayer = value;
    }

    public static Tag ShowRueiHint(
        this Player player, 
        string hint, 
        float duration, 
        float verticalPosition = 400f, 
        Tag tag = null!)
    {
        tag ??= new Tag();
        RueDisplay.Get(player).Show(tag, new BasicElement(verticalPosition, hint), duration);
        Timing.CallDelayed(duration + 0.2f, () => RueDisplay.Get(player).Update()); 
        return tag;
    }
}