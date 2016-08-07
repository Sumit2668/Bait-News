using System;
using UIKit;
using MikeCodesDotNET.iOS;

namespace BaitNews.iOS
{
	public partial class ResultsViewController : UIViewController
    {
		public ResultsViewModel ViewModel { get; set; }

        public ResultsViewController (IntPtr handle) : base (handle)
        {
			
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            lblWrong.Alpha = 0;
            lblCorrect.Alpha = 0;
            imgTick.Alpha = 0;
            imgCross.Alpha = 0;

			lblWrong.Text = $"{ViewModel.WrongCount} Wrong";
			lblCorrect.Text = $"{ViewModel.CorrectCount} Correct";
        }

        public override void ViewDidAppear(bool animated)
        {
            lblCorrect.FadeIn(0.2, 0);
            imgTick.FadeIn(0.2, 0);

            lblWrong.FadeIn(0.2, 0.3f);
            imgCross.FadeIn(0.2, 0.3f);

        }

        partial void BtnClose_TouchUpInside(UIButton sender)
        {
            var navigationController = UIApplication.SharedApplication.KeyWindow.RootViewController as UINavigationController;
            navigationController.DismissModalViewController(true);
        }
    }
}