using Telegram.Bot;
using Telegram.Bot.Types;

namespace LionCbdShop.TelegramBot.Extensions
{
    public static class TelegramBotClientExtensions
    {
        public static async Task SetWebAppInChatMenuButton(this ITelegramBotClient telegramBotClient, string buttonText, string webAppUrl)
        {
            var menuButtonWebApp = new MenuButtonWebApp()
            {
                Text = buttonText,
                WebApp = new WebAppInfo()
                {
                    Url = webAppUrl
                }
            };

            await telegramBotClient.SetChatMenuButtonAsync(menuButton: menuButtonWebApp);
        }
    }
}
