using System;
using AppServiceHelpers;

namespace BaitNews
{
	public static class App
	{
		public static EasyMobileServiceClient Client { get; }
		static App()
		{
			//Create our App Service Easy Client 
			Client = new EasyMobileServiceClient();
			Client.Initialize(Keys.AzureServiceUrl);

			//Register our objects
			Client.RegisterTable<Headline>();
			Client.FinalizeSchema();
		}
	}
}

