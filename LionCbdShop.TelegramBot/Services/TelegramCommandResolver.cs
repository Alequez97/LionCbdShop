using LionCbdShop.TelegramBot.Commands;
using LionCbdShop.TelegramBot.Interfaces;
using Telegram.Bot.Types;

namespace LionCbdShop.TelegramBot.Services;

public class TelegramCommandResolver : ITelegramCommandResolver
{
    private readonly IEnumerable<ITelegramCommand> _commands;
    private readonly UnknownCommand _unknownCommand;

    public TelegramCommandResolver(IEnumerable<ITelegramCommand> commands, UnknownCommand unknownCommand)
    {
        _commands = commands;
        _unknownCommand = unknownCommand;
    }

    public ITelegramCommand Resolve(Update update)
    {
        var command = _commands.FirstOrDefault(command => command.IsResponsibleForUpdate(update));

        if (command == null)
        {
            return _unknownCommand;
        }

        return command;
    }
}
