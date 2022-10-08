using System.Text.RegularExpressions;
using System.Text;

namespace LionCbdShop.TelegramBot.Services;

public class EmojiProvider
{
    private readonly Dictionary<string, string> _emojiMap = new Dictionary<string, string>()
    {
        { "banana", "\xF0\x9F\x8D\x8C" },
        { "grape", "\xF0\x9F\x8D\x87" },
        { "strawberry", "\xF0\x9F\x8D\x93" }
    };

    public string GetEmoji(string key)
    {
        if (_emojiMap.TryGetValue(key.ToLower(), out var emojiUtf8Code))
        {
            return Encoding.UTF8.GetString(Array.ConvertAll(Regex.Unescape(emojiUtf8Code).ToCharArray(), c => (byte)c));
        }

        return string.Empty;
    }
}
