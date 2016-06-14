using System;
using OperationLog.Presentation.Desktop.ViewModel;

namespace OperationLog.Presentation.Desktop.View
{
    public partial class StartWindow
    {
        private readonly StartWindowViewModel _startWindowViewModel = new StartWindowViewModel();

        public StartWindow()
        {
            InitializeComponent();
            DataContext = _startWindowViewModel;
        }

        protected override void OnClosed(EventArgs e)
        {
            _startWindowViewModel.Dispose();
            base.OnClosed(e);
        }
    }
}