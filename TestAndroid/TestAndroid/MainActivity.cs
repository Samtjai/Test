using System;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace TestAndroid
{
	[Activity (Label = "Let the game begin ", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		int count = 1, count1 =1 , firsttime =0;
		private System.Timers.Timer _timer;
		private int _countSeconds =0;
		public  Button button;
		public Button button1;
		public Button button2;

	
		protected override void OnCreate (Bundle bundle)
		{

			base.OnCreate (bundle);


			SetContentView (Resource.Layout.Main); 


			button = FindViewById<Button> (Resource.Id.myButton);
			button1 = FindViewById<Button> (Resource.Id.button1);
			button2 = FindViewById<Button> (Resource.Id.button2);

			button.Click += delegate {
			button.Text = string.Format ("Player 1    {0} clicks!", count++);
			};

			DisabledAll_button ();

			button1.Click += delegate {
				button1.Text = string.Format ("Player 2    {0} clicks!", count1++);
			};

			button2.Click += delegate {
				if (firsttime ==0 && _countSeconds ==0)
					{
						EnabledAll_button();
						button.Text = string.Format ("Player 1    {0} clicks!", 0);
						button1.Text = string.Format ("Player 2    {0} clicks!", 0);
						_timer = new System.Timers.Timer();
						//Trigger event every second
						_timer.Interval = 1000;
						_timer.Elapsed += OnTimedEvent;
						//count down 10 seconds
						_countSeconds = 10;

						_timer.Enabled = true;
						firsttime++;
					}
				else
					{
						if(firsttime ==1)
							{
								DisabledAll_button();
								_timer.Stop();
								firsttime++;
							}
						else
							{
								EnabledAll_button();
								_timer.Start();
								firsttime--;
							}

					}
					
			};

		}
		 

		private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
		{

			_countSeconds--;

			RunOnUiThread(delegate {

				button2.Text = string.Format ("Pauze \n 00:{0}", _countSeconds);

				if (_countSeconds == 0)
					{
						_timer.Stop();
						firsttime=0;
						DisabledAll_button();
						count = 1;
						count1 =1;
						button2.Text = string.Format ("Reset \n Start again");

					}


			});
		


		}
		public void DisabledAll_button(){
			button1.Enabled = false;
			button.Enabled = false;

		}
		public void EnabledAll_button(){
			button1.Enabled = true;
			button.Enabled = true;

		}
	}
}
