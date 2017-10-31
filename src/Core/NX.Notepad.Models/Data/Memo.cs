using System;

namespace NX.Notepad.Data
{
    public class Memo
    {
        /// <summary>
        /// IDを取得または設定します。
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// タイトルを取得または設定します。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 本文を取得または設定します。
        /// </summary>
        public string Body { get; set; }
    }
}