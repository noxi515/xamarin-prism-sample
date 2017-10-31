using JetBrains.Annotations;
using NLog;
using NX.Notepad.Util;
using Prism.Mvvm;
using Prism.Navigation;

namespace NX.Notepad.ViewModels
{
    public abstract class PageViewModel : BindableBase, INavigationAware
    {
        [NotNull]
        protected ILogger Log { get; }

        protected PageViewModel()
        {
            Log = LogManager.GetLogger(GetType().Name);
        }

        public virtual void OnNavigatedFrom([NotNull] NavigationParameters parameters)
        {
            Log.Trace(() => $"OnNavigatedFrom: parameters={parameters.ToJson()}");
        }

        public virtual void OnNavigatedTo([NotNull] NavigationParameters parameters)
        {
            Log.Trace(() => $"OnNavigatedTo: parameters={parameters.ToJson()}");
        }

        public virtual void OnNavigatingTo([NotNull] NavigationParameters parameters)
        {
            Log.Trace(() => $"OnNavigatingTo: parameters={parameters.ToJson()}");
        }
    }
}