using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NX.Notepad.Data;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace NX.Notepad.ViewModels
{
    public class MemoEditPageViewModel : PageViewModel
    {
        private readonly INavigationService _navigation;
        private readonly IPageDialogService _dialog;
        private readonly IMemoRepository _repository;

        private Memo _memo;
        private string _title = "";
        private string _body = "";
        private DelegateCommand _doneCommand;

        /// <summary>
        /// 一度でも編集を行ったかどうかのフラグ
        /// </summary>
        private bool _edited;


        /// <summary>
        /// メモの件名を取得または設定します。
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value);
                _edited = true;

                DoneCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// メモの本文を取得または設定します。
        /// </summary>
        public string Body
        {
            get => _body;
            set
            {
                SetProperty(ref _body, value);
                _edited = true;

                DoneCommand.RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// メモ作成コマンドを取得します。
        /// </summary>
        public DelegateCommand DoneCommand
        {
            get
            {
                if (_doneCommand != null)
                {
                    return _doneCommand;
                }

                return _doneCommand = new DelegateCommand(
                    async () =>
                    {
                        // 記入したメモを保存して、ダイアログを表示して、元のページに戻る
                        var memo = await PersistMemoAsync();
                        await _dialog.DisplayAlertAsync("メモを保存しました。", null, "OK");
                        await _navigation.GoBackAsync(new NavigationParameters
                        {
                            {"result", "success"},
                            {"id", memo.Id}
                        });
                    },
                    // 一度でも編集した、タイトルや本文が空では無ければコマンド実行可能
                    () => _edited && !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Body));
            }
        }

        public MemoEditPageViewModel(
            [NotNull] INavigationService navigationService,
            [NotNull] IPageDialogService dialog,
            [NotNull] IMemoRepository repository)
        {
            _navigation = navigationService;
            _dialog = dialog;
            _repository = repository;
        }

        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            
            // このページが最初に表示されたときのみ処理を行う
            if (parameters.GetNavigationMode() != NavigationMode.New)
            {
                return;
            }

            // 新規
            if (!parameters.ContainsKey("id"))
            {
                // Do nothing
                return;
            }

            //編集
            if (!Guid.TryParse(parameters["id"] as string, out var id))
            {
                return;
            }

            await DisplayMemoAsync(id);
        }

        private async Task DisplayMemoAsync(Guid id)
        {
            // メモが保存されていなかった
            if ((_memo = await _repository.GetAsync(id)) == null)
            {
                return;
            }

            Title = _memo.Title;
            Body = _memo.Body;
            _edited = false;
        }

        private async Task<Memo> PersistMemoAsync()
        {
            var memo = new Memo
            {
                Title = Title,
                Body = Body
            };
            if (_memo?.Id != null)
            {
                memo.Id = _memo.Id.Value;
            }

            await _repository.AddOrUpdateAsync(memo);
            return memo;
        }
    }
}