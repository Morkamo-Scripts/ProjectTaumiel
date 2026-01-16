using System;
using CommandSystem;

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