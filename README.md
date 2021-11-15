# User Registration Telegram Bot

[![package](https://img.shields.io/nuget/vpre/Telegram.Bot.svg?label=Telegram.Bot&style=flat-square)](https://www.nuget.org/packages/Telegram.Bot)
[![Bot API Version](https://img.shields.io/badge/Bot%20API-5.4%20(November%205,%202021)-f36caf.svg?style=flat-square)](https://core.telegram.org/bots/api)
[![documentations](https://img.shields.io/badge/Documentations-Book-orange.svg?style=flat-square)](https://telegrambots.github.io/book/)
[![telegram chat](https://img.shields.io/badge/Support_Chat-Telegram-blue.svg?style=flat-square)](https://t.me/joinchat/B35YY0QbLfd034CFnvCtCA)

[![build](https://img.shields.io/azure-devops/build/tgbots/14f9ab3f-313a-4339-8534-e8b96c7763cc/6?style=flat-square&label=master)](https://dev.azure.com/tgbots/Telegram.Bot/_build/latest?definitionId=6&branchName=master)
[![build](https://img.shields.io/azure-devops/build/tgbots/14f9ab3f-313a-4339-8534-e8b96c7763cc/10/develop?style=flat-square&label=develop)](https://dev.azure.com/tgbots/Telegram.Bot/_build/latest?definitionId=10&branchName=develop)
[![downloads](https://img.shields.io/nuget/dt/Telegram.Bot.svg?style=flat-square&label=Package%20Downloads)](https://www.nuget.org/packages/Telegram.Bot)
[![contributors](https://img.shields.io/github/contributors/TelegramBots/Telegram.Bot.svg?style=flat-square&label=Contributors)](https://github.com/TelegramBots/Telegram.Bot/graphs/contributors)
[![license](https://img.shields.io/github/license/TelegramBots/telegram.bot.svg?style=flat-square&maxAge=2592000&label=License)](https://raw.githubusercontent.com/TelegramBots/telegram.bot/master/LICENSE)

User Registration Telegram Bot Using (.NET Core 3.1 and MySQL 5.7) and Telegram Bot API for C#.
**Telegram.Bot** is the most popular .NET Client for 🤖 [Telegram Bot API].
The Bot API is an HTTP-based interface created for developers keen on building bots for [Telegram].

## 🔨 Getting Started
### Requirement  
- Visual Studio 2019 or leter.
- .NET Core 3.1
- MySQL 5.7 or leter.
    
### Supported Platforms
- Windows 10 (21H2 or leter)
- Linux (Debian 9 or leter)
    
### Build & Run ✔
- Clone this reposity
- Open using Visual Studio IDE
- Press `F5` or `CTRL` + `F5`


### Configurations �
- Enter your API key into `Configuration.cs`:
```
    class Configurations
    {
        public static string ApiToken { get; private set; } = "<Your Token Here>";
    }
```
- Enter database cordinates into `Database\Connaction.cs`

```
    class Connaction
    {
        public static string ConnectionString { get; private set; } = "server=localhost;user=root;database=userbotdb;port=3306;password=123456789";
        ...
     }
```

## Contribute 👋

**Your contribution is welcome!** 🙂
See [Contribution Guidelines](CONTRIBUTING.md).

## Note 👋
⚠ Project not complated yet
