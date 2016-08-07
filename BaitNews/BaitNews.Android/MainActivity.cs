using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Com.Wenchao.Cardstack;

namespace BaitNews.Droid
{
	[Activity(Label = "Bait News", MainLauncher = true, Icon = "@mipmap/ic_launcher")]
	public class MainActivity : BaseActivity
    {

        protected override int LayoutResource => Resource.Layout.main;

		CardStack cardStack;
		CardsDataAdapter adapter;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			cardStack = FindViewById<CardStack>(Resource.Id.container);

			cardStack.SetContentResource(Resource.Layout.item_card);
			cardStack.SetStackMargin(20);


			adapter = new CardsDataAdapter(this, Resource.Id.content, new string[]
			{
				"Test1", "Test2", "Test3", "Test4", "Test5"
			});


			cardStack.Adapter = adapter;

			SupportActionBar?.SetDisplayHomeAsUpEnabled(false);
			SupportActionBar?.SetHomeButtonEnabled(false);
		}


		class CardsDataAdapter : ArrayAdapter<string>
		{
			public CardsDataAdapter(Context context, int textViewResourceId, string[] objects)
				: base(context, textViewResourceId, objects)
			{
			}

			public override Android.Views.View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
			{
				CardsViewHolder holder = null;
				var view = convertView;
				holder = view?.Tag as CardsViewHolder;

				if (holder == null)
				{
					holder = new CardsViewHolder();
					view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_card, parent, false);

					holder.Content = view.FindViewById<TextView>(Resource.Id.content);
					view.Tag = holder;
				}

				holder.Content.Text = GetItem(position);

				return view;
			}
		}

		class CardsViewHolder : Java.Lang.Object
		{
			public TextView Content { get; set; }
		}
	}
}


