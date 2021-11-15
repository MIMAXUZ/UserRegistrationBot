using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace UCompanents
{
    class Companents
    {
        public static InlineKeyboardMarkup InlineKeyboard(Dictionary<int, string> items)
        {
            InlineKeyboardButton[][] ik = items.Select(item => new[]
            {
                InlineKeyboardButton.WithCallbackData(text: item.Value, callbackData: item.Key.ToString()),
                //new InlineKeyboardButton(item.Key, item.Value)
            }).ToArray();
            return new InlineKeyboardMarkup(ik);
        }

        public static ReplyKeyboardMarkup GetKeyboard(Dictionary<string, string> data)
        {
            ReplyKeyboardMarkup BtnKeyBoard = new ReplyKeyboardMarkup();
            var rows = new List<KeyboardButton[]>();
            var cols = new List<KeyboardButton>();
            foreach (var item in data)
            {
                int index = 0;
                cols.Add(new KeyboardButton(" " + item.Value));
                if (index % 4 != 0) continue;
                rows.Add(cols.ToArray());
                cols = new List<KeyboardButton>();
                index++;
            }
            //for (var Index = 0; Index < data.Count; Index++)
            //{
            //    cols.Add(new KeyboardButton(data[Index]));
            //    if (Index % 4 != 0) continue;
            //    rows.Add(cols.ToArray());
            //    cols = new List<KeyboardButton>();
            //}
            BtnKeyBoard.Keyboard = rows.ToArray();
            return BtnKeyBoard;
        }
    }
}
