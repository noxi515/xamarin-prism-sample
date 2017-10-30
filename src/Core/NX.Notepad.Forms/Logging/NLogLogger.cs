using NLog;
using Prism.Logging;

namespace NX.Notepad.Logging
{
    /// <summary>
    /// Prismの実行ログをNLogへバイパスするクラス
    /// </summary>
    public sealed class NLogLogger : ILoggerFacade
    {
        private static readonly ILogger Logger = LogManager.GetLogger("Prism");

        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    Logger.Debug(message);
                    break;

                case Category.Info:
                    Logger.Info(message);
                    break;

                case Category.Warn:
                    Logger.Warn(message);
                    break;

                case Category.Exception:
                    Logger.Error(message);
                    break;
            }
        }
    }
}