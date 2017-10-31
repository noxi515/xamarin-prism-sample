using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using NX.Notepad.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace NX.Notepad.ViewModels
{
    public class MemoListPageViewModel : PageViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IMemoRepository _repository;

        private DelegateCommand _memoAddCommand;
        private DelegateCommand<MemoItemViewModel> _memoEditCommand;
        private DelegateCommand<MemoItemViewModel> _memoDeleteCommand;

        /// <summary>
        /// メモ一覧を取得します。
        /// </summary>
        public ObservableCollection<MemoItemViewModel> Memos { get; } = new ObservableCollection<MemoItemViewModel>();

        /// <summary>
        /// メモを追加するコマンドを取得します。
        /// </summary>
        public DelegateCommand MemoAddCommand
        {
            get
            {
                if (_memoAddCommand != null)
                {
                    return _memoAddCommand;
                }

                return _memoAddCommand = new DelegateCommand(
                    async () => await _navigation.NavigateAsync("MemoEditPage"));
            }
        }

        /// <summary>
        /// メモをタップしたときのコマンドを取得します。
        /// </summary>
        public DelegateCommand<MemoItemViewModel> MemoEditCommand
        {
            get
            {
                if (_memoEditCommand != null)
                {
                    return _memoEditCommand;
                }

                return _memoEditCommand = new DelegateCommand<MemoItemViewModel>(
                    async vm => await _navigation.NavigateAsync($"MemoEditPage?id={vm.Memo.Id:N}"));
            }
        }

        /// <summary>
        /// メモを削除するコマンドを取得します。
        /// </summary>
        public DelegateCommand<MemoItemViewModel> DeleteMemoCommand
        {
            get
            {
                if (_memoDeleteCommand != null)
                {
                    return _memoDeleteCommand;
                }

                return _memoDeleteCommand = new DelegateCommand<MemoItemViewModel>(
                    async vm =>
                    {
                        if (vm.Memo.Id == null)
                        {
                            return;
                        }

                        var result = await _repository.DeleteAsync(vm.Memo.Id.Value);
                        if (result)
                        {
                            Memos.Remove(vm);
                        }
                    });
            }
        }

        public MemoListPageViewModel(INavigationService navigationService, IMemoRepository repository)
        {
            _navigation = navigationService;
            _repository = repository;
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            // 新規ページ遷移ではない、または遷移先ページからの結果が成功ではない
            if (parameters.GetNavigationMode() != NavigationMode.New && parameters["result"] as string != "success")
            {
                return;
            }

            Memos.Clear();
            (await _repository.GetAllAsync())
                .Select(memo => new MemoItemViewModel(memo))
                .Reverse()
                .ForEach(Memos.Add);
        }

        public class MemoItemViewModel : BindableBase
        {
            public Memo Memo { get; }

            public string Title => Memo.Title;

            public MemoItemViewModel(Memo memo)
            {
                Memo = memo;
            }
        }
    }
}