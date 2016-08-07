﻿using System.Collections.Generic;

using CoreGraphics;
using UIKit;

using System.Linq;

namespace BaitNews.iOS
{
    public class CardHolderView : UIView
    {
        //Default Values
        int defaultCountOfVisibleCards = 3;

        public CardHolderView(CGRect rect, List<Headline> headlines)
        {
            Frame = rect;
            Headlines = headlines;
            visibleCards = new List<HeadlineView>();

            var i = 0;
            while (i != defaultCountOfVisibleCards)
            {
                var headline = new HeadlineView(headlines[i]);
                headline.OnSwipe += HandleOnSwipe;

                visibleCards.Add(headline);
                i++;
            }

            LayoutCards();
        }

        List<HeadlineView> visibleCards;
        public List<HeadlineView> VisibleCards
        {
            get
            {
                return visibleCards;
            }
        }

        public int NumberOfCards
        {
            get
            {
                return Headlines.Count;
            }
        }

        void LayoutCards()
        {
            foreach (var card in visibleCards)
            {
                var indexPosition = visibleCards.IndexOf(card);

                card.Center = new CGPoint(Center.X, Center.Y - 100);
                card.Bounds = new CGRect(0f, 0f, (int)Bounds.Width - 40f, (int)Bounds.Height - 20f);
                switch (indexPosition)
                {
                    case 0:
                        AddSubview(card);
                        break;
                    case 1:
                        InsertSubviewBelow(card, visibleCards[0]);
                        break;
                    case 2:
                        InsertSubviewBelow(card, visibleCards[1]);
                        break;
                }
            }
            UpdateCardsPosition();
        }

        public HeadlineView ViewForCardAtIndex(int index)
        {
            var view = new HeadlineView(Headlines[index]);
            return view != null ? view : null;
        }

        public List<Headline> Headlines { get; private set;}

        public Headline VisibleHeadline
        {
            get
            {
                return VisibleCards.FirstOrDefault().Headline;
            }
        }

        public void Clear()
        {
            foreach (var card in visibleCards)
            {
                card.RemoveFromSuperview();
            }

            Headlines.Clear();
        }

        HeadlineView TopCard
        {
            get
            {
                return visibleCards[0];
            }
        }

        void HandleOnSwipe(object sender, DraggableEventArgs args)
        {
            var headlineView = sender as HeadlineView;
            Headlines.Remove(headlineView.Headline);

            if (args.Dragged.Equals(DraggableDirection.None))
                return;

            if (args.Dragged.Equals(DraggableDirection.Left) && DidSwipeLeft != null)
            {
                DidSwipeLeft(headlineView);
            }
            else if (args.Dragged.Equals(DraggableDirection.Right) && DidSwipeRight != null)
            {
                DidSwipeRight(headlineView);
            }
            headlineView.RemoveFromSuperview();
            InsertSubviewBelow(headlineView, visibleCards.Last());
            visibleCards.Remove(headlineView);
            visibleCards.Add(headlineView);

            LoadNextCard();
        }

        void LoadNextCard()
        {
            if (Headlines == null || Headlines.Count == 0)
            {
                NoMoreCards();
                return;
            }

            var cardView = visibleCards.Last();
            cardView.Center = new CGPoint(Center.X, Center.Y - 100);
            cardView.Bounds = new CGRect(0f, 0f, (int)Bounds.Width - 40f, (int)Bounds.Height - 20f);
            cardView.Headline = Headlines.Count > 3 ? Headlines[visibleCards.Count] : Headlines[0];

            UpdateCardsPosition();
        }

        void UpdateCardsPosition()
        {
            foreach (var card in visibleCards)
            {
                var indexPosition = visibleCards.IndexOf(card);
                UIView.Animate(0.2, 0, UIViewAnimationOptions.CurveEaseIn, () =>
               {
                   switch (indexPosition)
                   {
                       case 0:
                           card.Center = new CGPoint(Center.X, Center.Y - 100);
                           card.Bounds = new CGRect(0f, 0f, (int)Bounds.Width - 40f, (int)Bounds.Height - 20f);
                           break;

                       case 1:
                           card.Center = new CGPoint(Center.X, Center.Y - 95);
                           card.Bounds = new CGRect(0f, 0f, (int)Bounds.Width - 44f, (int)Bounds.Height - 20f);
                           break;

                       case 2:
                           card.Center = new CGPoint(Center.X, Center.Y - 90);
                           card.Bounds = new CGRect(0f, 0f, (int)Bounds.Width - 48f, (int)Bounds.Height - 20f);
                           break;
                   }   
               }, () => { });
            }
        }

        //Events
        public delegate void OnSwipeLeftHandler(HeadlineView sender);
        public event OnSwipeLeftHandler DidSwipeLeft;

        public delegate void OnSwipeRightHandler(HeadlineView sender);
        public event OnSwipeRightHandler DidSwipeRight;

        public delegate void OnNoMoreCardsHandler();
        public event OnNoMoreCardsHandler NoMoreCards;
    }
}

