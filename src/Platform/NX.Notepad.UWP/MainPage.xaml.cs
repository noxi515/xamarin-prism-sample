namespace NX.Notepad.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new Notepad.App());
        }
    }
}
