using NX.Notepad.Logging;
using NX.Notepad.Views;
using Prism.Logging;
using Prism.Mvvm;
using Prism.Unity;
using Xamarin.Forms;

namespace NX.Notepad
{
    public partial class App
    {
        protected override async void OnInitialized()
        {
            // アプリ初期化が終わったタイミングで呼ばれるメソッド

            InitializeComponent();

            // トップページへ遷移
            await NavigationService.NavigateAsync("/NavigationPage/MemoListPage");
        }

        protected override void RegisterTypes()
        {
            // Views
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MemoListPage>();
        }

        protected override ILoggerFacade CreateLogger()
        {
            // Prismの内部ログをNLogへバイパス
            return new NLogLogger();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            // ViewとViewModelの紐付け
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(ViewTypeToViewModelTypeResolver.Resolve);
        }
    }
}
