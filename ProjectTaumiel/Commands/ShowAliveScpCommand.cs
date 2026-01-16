using System;
using CommandSystem;
using Exiled.Permissions.Extensions;
using ProjectTaumiel.Components.Extensions;

namespace ProjectTaumiel.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class ShowAliveScpCommand : ICommand
{
    public string Command { get; } = "showAliveScps";
    public string[] Aliases { get; } = ["showAS"];
    public string Description { get; } = "Включает и выключает показ живых SCP!";
    
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission("pt.showAS"))
        {
            string requestPermission = "Требуется разрешение - 'pt.showAS'";
            
            if (Plugin.Instance.Config.Debug)
                response = $"<color=red>Вы не имеете права использовать данную команду!</color>\n" +
                           $"<color=orange>[{requestPermission}]</color>";
            else
                response = "<color=red>Вы не имеете права использовать данную команду!</color>";
            
            return false;
        }
        
        if (arguments.Count < 1 || !byte.TryParse(arguments.At(0), out byte state))
        {
            response = "Формат ввода: showas [1/0]. (Пример: showas 1)";
            return false;
        }

        if (state != 1 && state != 0)
        {
            response = "Некорректное значение состояния. Либо 1 (вкл.), либо 0 (выкл.)!";
            return false;
        }

        sender.AsPlayer().TaumielProperties().PlayerProperties
            .IsShowAliveScps = state == 1;
        
        DatabaseHandler.RemovePlayer(arguments.At(0));
        
        response = "Игрок успешно удален из списка Taumiel пользователей!";
        return true;
    }
}