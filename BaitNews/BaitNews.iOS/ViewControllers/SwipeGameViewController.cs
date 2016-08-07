using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using SafariServices;
using UIKit;

using MikeCodesDotNET.iOS;
using NotificationHub;
using AppServiceHelpers;

namespace BaitNews.iOS
{
	public partial class SwipeGameViewController : UIViewController
    {
		SwipeGameViewModel ViewModel { get; }
        Notifier incorrectHub;
        Notifier correctHub;
        CardHolderView cardHolder;

        
        const string segueIdentifier = "RESULTS_SEGUE_IDENTIFIER";

        public SwipeGameViewController(IntPtr handle) : base(handle)
        {
			ViewModel = new SwipeGameViewModel();
        }

        async public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            btnRead.Alpha = 0;

            incorrectHub = new Notifier(btnIncorrect);
            incorrectHub.MoveCircle(-48, -18);
            incorrectHub.SetCircleColor(btnIncorrect.TitleColor(UIControlState.Normal), UIColor.White);
            incorrectHub.ShowCount();

            correctHub = new Notifier(btnCorrect);
            correctHub.MoveCircle(-48, -18);
            correctHub.SetCircleColor(btnCorrect.TitleColor(UIControlState.Normal), UIColor.White);
            correctHub.ShowCount();

            btnCorrect.Alpha = 0;
            btnIncorrect.Alpha = 0;

			await ViewModel.LoadHeadlines();

            if (await Plugin.Connectivity.CrossConnectivity.Current.IsReachable("google.com"))
                btnRead.Alpha = 1.0f;

			cardHolder = new CardHolderView(cardPlaceholder.Frame, ViewModel.Headlines.ToList());
            cardHolder.DidSwipeLeft += OnSwipeLeft;
            cardHolder.DidSwipeRight += OnSwipeRight;
            cardHolder.NoMoreCards += FinishGame;

            View.AddSubview(cardHolder);
        }

        async public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            var connected = await Plugin.Connectivity.CrossConnectivity.Current.IsReachable("google.com");
            if (connected)
                btnRead.FadeIn(1, 0);

        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == segueIdentifier)
            {
                var vc = (ResultsViewController)segue.DestinationViewController;
                if (vc == null)
                    return;

				vc.ViewModel = new ResultsViewModel(ViewModel.Answers);
            }
        }

        async partial void BtnRead_TouchUpInside(UIButton sender)
        {
			await ViewModel.ReadHeadline(cardHolder.VisibleHeadline);
        }

        partial void BtnFinish_TouchUpInside(UIButton sender)
        {
            //Could do something here..
        }

        void OnSwipeLeft(HeadlineView sender)
        {
            var card = sender;
            var headline = card.Headline;

            if (lblHelper.Alpha != 0)
                lblHelper.Alpha = 0;

            var answer = new Answer() { Headline = headline };

            //User believes headline to be false
            if (headline.IsTrue)
            {
                if (btnIncorrect.Alpha == 0)
                    btnIncorrect.FadeIn(0.6, 0.2f);
                
                incorrectHub.Increment(1, NotificationAnimationType.Pop);
                answer.CorrectAnswer = false;
            }
            else
            {
                if (btnCorrect.Alpha == 0)
                    btnCorrect.FadeIn(0.6, 0.2f);
                
                correctHub.Increment(1, NotificationAnimationType.Pop);
                answer.CorrectAnswer = true;
            }
			ViewModel.Answers.Add(answer);
        }

        void OnSwipeRight(HeadlineView sender)
        {
            var card = sender;
            var headline = card.Headline;

            if (lblHelper.Alpha != 0)
                lblHelper.Alpha = 0;
            
            var answer = new Answer() { Headline = headline };

            //User believes headline to be true
            if (headline.IsTrue)
            {
                if (btnCorrect.Alpha == 0)
                    btnCorrect.FadeIn(0.6, 0.2f);
                
                correctHub.Increment(1, NotificationAnimationType.Pop);
                answer.CorrectAnswer = true;
            }
            else
            {
                if (btnIncorrect.Alpha == 0)
                    btnIncorrect.FadeIn(0.6, 0.2f);
                
                incorrectHub.Increment(1, NotificationAnimationType.Pop);
                answer.CorrectAnswer = false;
            }
			ViewModel.Answers.Add(answer);

        }
        void FinishGame()
        {
            DismissViewController(true, null);
        }

    }
}