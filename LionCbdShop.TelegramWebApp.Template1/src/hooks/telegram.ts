const telegramWebApp = window.Telegram.WebApp

export function useTelegramWebApp() {
    telegramWebApp.ready();

    const showButton = () => {
        
    }

    return {
        telegramWebApp
    }
}