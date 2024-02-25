using StoryTellingBot.BotCommands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBotSandBox.Commands;

public class LongTermCommand(TelegramBotClient botClient) : ICommand
{
    private readonly List<string> _answers = new();

    public async Task Handle(Message message, CancellationToken cts)
    {
        var chatId = message.Chat.Id;
        await Task.Delay(5000, cts); // some fetch data;

        var questions = Enumerable.Range(0, 10).Select(x => $"Question{x}");
        var prevMessage = message.Text;
        foreach (var question in questions)
        {
            Message questionMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: question,
                cancellationToken: cts);
            while (true)
            {
                var newUpdates = await botClient.GetUpdatesAsync(offset: -1, cancellationToken: cts);
                var currMessage = newUpdates[^1].Message.Text;
                if (prevMessage != currMessage)
                {
                    _answers.Add(currMessage);
                    prevMessage = currMessage;
                    break;
                }

                await Task.Delay(500, cts).ConfigureAwait(false);
            }
        }

        var answersString = string.Join(", ", _answers);
        Message finalMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"Відповіді: {answersString} \n Генерується розповідь",
            cancellationToken: cts);

        await Task.Delay(15000, cts);

        Message storyMessage = await botClient.SendTextMessageAsync(
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