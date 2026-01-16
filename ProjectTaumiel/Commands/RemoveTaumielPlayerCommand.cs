using System;
using CommandSystem;

namespace ProjectTaumiel.Commands;

[CommandHandler(typeof(ClientCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class RemoveTaumielPlayerCommand : ICommand
{
    public string Command { get; } = "removeTaumielPlayer";
    public string[] Aliases { get; } = ["rmtmpl"];
    public string Description { get; } = "Удаляет игрока из списка Taumiel пользователей!";
    
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count < 1)
        {
            response = "Формат ввода: rmtmpl [id64]. (Пример: rmtmpl 7777777777777@steam)";
            return false;
        }

        if (!DatabaseHandler.IsPlayerExists(arguments.At(0)))
        {
            response = "Игрока и так нет в списке Taumiel пользователей!";
            return false;
        }
        
        DatabaseHandler.RemovePlayer(arguments.At(0));
        
        response = "Игрок успешно удален из списка Taumiel пользователей!";
        return true;
    }
}