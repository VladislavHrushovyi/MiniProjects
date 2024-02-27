using StoryTellingBot.BotCommands;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBotSandBox.Repository;

namespace TgBotSandBox.Commands;

public class LongTermCommand(ITelegramBotClient botClient, IChatRepository chatRepository) : ICommand
{
    public async Task Handle(Message message, CancellationToken cts)
    {
        //fetch some data from internet
        //create state for this command
        //add to state fetched data
    }
}