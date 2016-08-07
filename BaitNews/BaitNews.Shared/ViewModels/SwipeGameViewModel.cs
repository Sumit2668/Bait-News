using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppServiceHelpers;

using Plugin.Share;

namespace BaitNews
{
	public class SwipeGameViewModel
	{
		public ConnectedObservableCollection<Headline> Headlines { get; }
		public List<Answer> Answers { get; } = new List<Answer>();
		public SwipeGameViewModel()
		{
			Headlines = new ConnectedObservableCollection<Headline>(App.Client.Table<Headline>());
		}


		public async Task LoadHeadlines()
		{
			await Headlines.Refresh();
			Headlines.Shuffle();
		}

		public Task ReadHeadline(Headline headline)
		{
			if (headline == null)
				return Task.FromResult(true);
			
			return CrossShare.Current.OpenBrowser(headline.Url, new Plugin.Share.Abstractions.BrowserOptions
			{
				ChromeShowTitle = false,
				UseSafairReaderMode = true,
				UseSafariWebViewController = true
			});
		}
	}
}

