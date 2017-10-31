using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NX.Notepad.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MemoListPage
    {
        public MemoListPage()
        {
            InitializeComponent();
        }

        private void OnMemoSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Disable item selection
            ((ListView) sender).SelectedItem = null;
        }
    }
}