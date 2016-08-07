using System;
using UIKit;

namespace BaitNews.iOS
{
	public partial class SettingsViewController : UITableViewController
    {
		SettingsViewModel viewModel;
        public SettingsViewController (IntPtr handle) : base (handle)
        {
			viewModel = new SettingsViewModel();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			lblBuildVersion.Text = $"Version {viewModel.VersionNumber} build {viewModel.BuildNumber}";
        }
    }
}