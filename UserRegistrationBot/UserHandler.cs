using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Framework;
using System.Threading.Tasks;
using System.Linq;
using Telegram.Bot.Types;
using UDatabase;
using UCompanents;
using UUser;
using System.IO;
using System.Threading;
using Telegram.Bot.Requests;
using System.Net;
using Telegram.Bot.Types.Enums;

namespace UserRegistrationBot
{
    class UserHandler
    {
        //Global variables
        //start step counter
        public static int SigunUpSteps = 0;
        public static string CallBackId = "";
        static TelegramBotClient _regBot = new TelegramBotClient(Configurations.ApiToken);

        /// <summary>
        /// Use this method to get Startup.
        /// </summary>
        [Obsolete]
        public static void StartUp()
        {
            _regBot.StartReceiving();
            _regBot.OnMessage += Bot_OnMessage;
            _regBot.OnCallbackQuery += Bot_OnCallbackQuery;
            Console.ReadLine();
        }

        /// <summary>
        /// Use this method to get a call back data.
        /// </summary>
        [Obsolete]
        private static async void Bot_OnCallbackQuery(object test, CallbackQueryEventArgs e)
        {
            try
            {
                // Console.WriteLine("On CallBac");
                if (e.CallbackQuery.Data != null && SigunUpSteps == 5)
                {
                    //string res = "Hudud tanlandi.";
                    //await SendMessageTextForCallBackAsync(res, e, 0);
                    // Answer with null parameter:
                    await _regBot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Hudud tanlandi");
                    var inlineKeyboardMarkup = Companents.InlineKeyboard(Components.GetOrganizations());
                    await _regBot.SendTextMessageAsync(
                        chatId: e.CallbackQuery.Message.Chat, text: "Iltimos joriy tashkilotingizni tanlang!",
                        replyMarkup: inlineKeyboardMarkup);

                    UserInfo.Location = Convert.ToInt32(e.CallbackQuery.Data);
                    SigunUpSteps += 1;
                    //string res = "Hudud tanlandi.";
                    //await SendMessageTextForCallBackAsync(res, e, 1);
                }

                // Organization selector
                if (e.CallbackQuery.Data != null && SigunUpSteps == 6 && !e.CallbackQuery.Message.Text.Contains("hududingizni tanlang"))
                {
                    UserInfo.Organization = Convert.ToInt32(e.CallbackQuery.Data);

                    if (Components.InserApplication(UserInfo.FirstName, UserInfo.LastName, UserInfo.MiddleName, UserInfo.Photo, UserInfo.Location, UserInfo.Organization))
                    {
                        string reply = "Tabriklaymiz! Siz ro'yxatdan o'tdingiz! \n " + " Ro'yxatga olingan ma'lumotlar \n" + 
                            "FIO: " + UserInfo.FirstName + " " + UserInfo.LastName + " " + UserInfo.MiddleName + " \n";
                        // Answer with null parameter:
                        await _regBot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, null);
                        // Reply with message instead:
                        await _regBot.SendTextMessageAsync(e.CallbackQuery.Message.Chat, reply);
                        SigunUpSteps += 1;
                    }
                    else
                    {
                        string reply = "Xatolik yuz berdi! Iltimos birozdan keyin qaytra urunib ko'ring.";
                        // Answer with null parameter:
                        await _regBot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, "Xatolik!");
                        // Reply with message instead:
                        await _regBot.SendTextMessageAsync(e.CallbackQuery.Message.Chat, reply);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Use this method to fatch user sent message
        /// </summary>
        [Obsolete]
        private static async void Bot_OnMessage(object test, MessageEventArgs e)
        {
            try
            {
                //Console.WriteLine(e.Message.Type.ToString());
                if (e.Message.Type.ToString() == "Text" || e.Message.Type.ToString() == "Document")
                    HandleMessageResponse(e);
                else
                {
                    var send_error_response = $"Uzr {e.Message.Chat.FirstName}! Siz ruxsat etilmagan fayl yoki matn yubordingiz! Iltimos ruxsat etilgan formatdagi matnni yoki faylni yuboring...";
                    await _regBot.SendTextMessageAsync(e.Message.Chat.Id, send_error_response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Take the Text Message and handle it as required.
        /// </summary>
        /// <param name="e"></param>
        [Obsolete]
        private static void HandleMessageResponse(MessageEventArgs e)
        {
            try
            {
                var messagetext = e.Message.Text;
                if (messagetext == "/start")
                {
                    SendStartResponse(e);
                    SigunUpSteps = 1;
                    //Console.WriteLine("Botdan foydalanishni boshlang!");
                }
                else
                {
                    SendListReponse(e, messagetext);
                    //Console.WriteLine("Javob yuborildi.");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Splits the message received into a string array and sends them all back.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="messagetext"></param>
        [Obsolete]
        private static async void SendListReponse(MessageEventArgs e, string messagetext)
        {
            try
            {
                char[] splitter = new[] { '\n' };
                string[] stringlist = { "" };
                if (messagetext != null)
                    stringlist = messagetext.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                //string[] stringlist = messagetext.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                //Console.WriteLine(e.Message.Chat.Id.ToString());
                if (stringlist.Length >= 2)
                {
                    var send_error_response = $"{e.Message.Chat.FirstName}, Siz bir qatordan ko'p bo'lgan matnni kiritdingiz! \n Iltimos faqat bir qatorda joylashtirilgan matnni jo'nating.";
                    await _regBot.SendTextMessageAsync(e.Message.Chat.Id, send_error_response);
                }
                else
                {
                    if (SigunUpSteps == 1)
                    {
                        if (stringlist[0].Contains("Ro'yxatdan o'tish"))
                        {
                            await _regBot.SendTextMessageAsync(
                                chatId: e.Message.Chat.Id,
                                text: "Iltimos ismingizni kiriting",
                                replyMarkup: new ReplyKeyboardRemove()
                            );
                        }
                        else if (!stringlist[0].Contains("Ro'yxatdan o'tish") && stringlist[0].Length >= 3)
                        {
                            UserInfo.FirstName = stringlist[0];
                            string res = "Familyangizni kiriting";
                            SendMessageText(res, e, 1);
                        }
                        else
                        {
                            string res = "Nimadir xato kiritildi! Iltimos tekshirib qaytadan urunib ko'ring!";
                            SendMessageText(res, e, 0);
                        }
                    }

                    //Go To Last Name
                    if (SigunUpSteps == 2)
                    {
                        if (!stringlist[0].Contains("Familyangizni kiriting") && stringlist[0].Length >= 3 && UserInfo.LastName == null)
                        {
                            UserInfo.LastName = stringlist[0];
                            string res = "Sharfingizni kiriting";
                            SendMessageText(res, e, 1);
                        }
                        else
                        {
                            string res = "Nimadir xato kiritildi! Iltimos tekshirib qaytadan urunib ko'ring!";
                            SendMessageText(res, e, 0);
                        }
                    }

                    //Go to middle name
                    if (SigunUpSteps == 3)
                    {
                        if (!stringlist[0].Contains("Sharfingizni kiriting") && stringlist[0].Length >= 3 && UserInfo.MiddleName == null)
                        {
                            UserInfo.MiddleName = stringlist[0];

                            string res = "Rasmningizni yuklang! \n" +
                                "<b>⚠️</b>: Rasm <b>fayl (jpg, png, jpeg)</b> ko'rinishida bo'lishi lozim!";
                            SendMessageText(res, e, 1);

                        }
                        else
                        {
                            string res = "Nimadir xato kiritildi! Iltimos tekshirib qaytadan urunib ko'ring!";
                            SendMessageText(res, e, 0);
                        }
                    }

                    //Go to Avatar 
                    if (SigunUpSteps == 4)
                    {
                        if (UserInfo.Photo == null && e.Message.Document != null)
                        {
                            string fileName = e.Message.MessageId + "_" + UserInfo.FirstName + "_" + UserInfo.LastName;
                            string fileType = ".png";
                            var file = await _regBot.GetFileAsync(e.Message.Document.FileId);
                            FileStream fs = new FileStream(@"C:\Games\" + fileName + fileType, FileMode.Create);
                            await _regBot.DownloadFileAsync(file.FilePath, fs);
                            fs.Close();
                            fs.Dispose();
                            UserInfo.Photo = fileName + fileType;

                            var inlineKeyboardMarkup = Companents.InlineKeyboard(Components.GetRegionList());
                            await _regBot.SendTextMessageAsync(
                                chatId: e.Message.Chat.Id, text: "Iltimos joriy hududingizni tanlang!",
                                replyMarkup: inlineKeyboardMarkup);

                            SigunUpSteps += 1;
                            //SendMessageText(res, e, 1);
                        }
                        else
                        {
                            string res = "Nimadir xato kiritildi! Iltimos tekshirib qaytadan urunib ko'ring!";
                            SendMessageText(res, e, 0);
                        }
                    }

                    if (SigunUpSteps == 1 && stringlist[0] != null && !stringlist[0].Contains("Ro'yxatdan o'tish") && stringlist[0].Length <= 3)
                    {
                        string res = "Iltimos to'g'ri ismni kiriting!";
                        SendMessageText(res, e, 0);
                    }

                    //for (var i = 0; i <= (stringlist.Length - 1); i++)
                    //{
                    //    await _regBot.SendTextMessageAsync(e.Message.Chat.Id, stringlist[i]);
                    //}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Sends out the first response a user gets
        /// </summary>
        /// <param name="e"></param>
        [Obsolete]
        private static async void SendStartResponse(MessageEventArgs e)
        {

            //string[] btns = { "Ro'yxatdan O'tish", "Yordam", "Bog'lanish" };
            var btns = new Dictionary<string, string>
            {
                { "0", "Ro'yxatdan o'tish" },
                { "1", "Yordam" },
                { "2", "Bog'lanish" }
            };
            var send_start_response = $"Salom! <b>{e.Message.Chat.FirstName}</b>!, \n " +
                $"men UserBot man! \n " +
                $"Men siz haqingizdagi ma'lumotlarni tizim administratoriga yuborish bilan shug'ullanaman. \n " +
                $"Jarayonni boshlash uchun iltimos <b>Ro'yxatdan o'tish</b> tugmachasini ni bosing.";
            await _regBot.SendTextMessageAsync(
                e.Message.Chat.Id,
                send_start_response,
                Telegram.Bot.Types.Enums.ParseMode.Html,
                replyMarkup: Companents.GetKeyboard(btns));

            //await _regBot.SendTextMessageAsync(e.Message.Chat.Id, send_start_response);
        }

        [Obsolete]
        private static async void SendMessageText(string msg, MessageEventArgs e, int val)
        {
            await _regBot.SendTextMessageAsync(e.Message.Chat.Id, msg, ParseMode.Html);
            SigunUpSteps += val;
        }

        [Obsolete]
        private static async Task SendMessageTextForCallBackAsync(string msg, CallbackQueryEventArgs e, int val)
        {
            await _regBot.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, msg);
            SigunUpSteps += val;
        }

    }
}
