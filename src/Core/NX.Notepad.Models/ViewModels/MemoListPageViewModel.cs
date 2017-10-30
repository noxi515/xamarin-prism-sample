using Prism.Mvvm;

namespace NX.Notepad.ViewModels
{
    public class MemoListPageViewModel : BindableBase
    {
        private string _text;

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }
    }
}