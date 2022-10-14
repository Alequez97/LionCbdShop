const telegramWebApp = window.Telegram.WebApp

export function useTelegramWebApp() {
    telegramWebApp.ready();

    return {
        telegramWebApp
    }
}