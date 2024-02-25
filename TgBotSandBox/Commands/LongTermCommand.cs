using StoryTellingBot.BotCommands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBotSandBox.Commands;

public class LongTermCommand(ITelegramBotClient botClient) : ICommand
{
    private readonly List<string> _answers = new();

    public async Task Handle(Message message, CancellationToken cts)
    {
        var chatId = message.Chat.Id;
        await Task.Delay(5000, cts); // some fetch data;

        var questions = Enumerable.Range(0, 10).Select(x => $"Question{x}");
        foreach (var question in questions)
        {
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: question,
                cancellationToken: cts);

            var oldUpdates = await botClient.GetUpdatesAsync(cancellationToken: cts);
            var oldUpdatesCountByChat = oldUpdates.Count(x => x.Message.Chat.Id == chatId);
            while (true)
            {
                var newUpdates = await botClient.GetUpdatesAsync(offset:20, cancellationToken: cts);
                var newUpdateCountByChat = newUpdates.Count(x => x.Message.Chat.Id == chatId);
                Console.WriteLine($"OldMessage={oldUpdates.Last().Message?.Text} NewCount={newUpdates.Last().Message?.Text}");
                if (oldUpdatesCountByChat < newUpdateCountByChat)
                {
                    var currMessage = newUpdates[^1].Message.Text;
                    _answers.Add(currMessage);
                    break;
                }

                await Task.Delay(500, cts);
            }
        }

        var answersString = string.Join(", ", _answers);
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Відповіді: {answersString} \n Генерується розповідь",
            cancellationToken: cts);

        await Task.Delay(15000, cts);

        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Генерується аудиозапис. \n Story={answersString}",
            cancellationToken: cts);

        await Task.Delay(10000, cts);
        
        await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Mp3 file is already",
            cancellationToken: cts);
    }
}