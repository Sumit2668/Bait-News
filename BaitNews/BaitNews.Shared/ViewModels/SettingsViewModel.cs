using System;
using Plugin.VersionTracking;
namespace BaitNews
{
	public class SettingsViewModel
	{
		public bool IsFirstRun => CrossVersionTracking.Current.IsFirstLaunchEver;

		public string VersionNumber => CrossVersionTracking.Current.CurrentVersion;

		public string BuildNumber => CrossVersionTracking.Current.CurrentBuild;
	}
}

