using System;
using CommandSystem;
using Exiled.Permissions.Extensions;

namespace ProjectTaumiel.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class AddTaumielPlayerCommand : ICommand
{
    public string Command { get; } = "addTaumielPlayer";
    public string[] Aliases { get; } = ["addtmpl"];
    public string Description { get; } = "Добавляет игрока в список Taumiel пользователей!";
    
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission("pt.addtmpl"))
        {
            string requestPermission = "Требуется разрешение - 'pt.addtmpl'";
            
            if (Plugin.Instance.Config.Debug)
                response = $"<color=red>Вы не имеете права использовать данную команду!</color>\n" +
                           $"<color=orange>[{requestPermission}]</color>";
            else
                response = "<color=red>Вы не имеете права использовать данную команду!</color>";
            
            return false;
        }
        
        if (arguments.Count < 1)
        {
            response = "Формат ввода: addtmpl [id64]. (Пример: addtmpl 7777777777777@steam)";
            return false;
        }

        if (DatabaseHandler.IsPlayerExists(arguments.At(0)))
        {
            response = "Игрок уже есть в списке Taumiel пользователей!";
            return false;
        }
        
        DatabaseHandler.AddPlayer(arguments.At(0));
        
        response = "Игрок успешно добавлен в список Taumiel пользователей!";
        return true;
    }
}