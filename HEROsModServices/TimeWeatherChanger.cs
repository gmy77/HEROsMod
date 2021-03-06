﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HEROsMod.UIKit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using HEROsMod.UIKit.UIComponents;

namespace HEROsMod.HEROsModServices
{
	class TimeWeatherChanger : HEROsModService
	{
		public static bool TimePaused { get; set; }
		private static double _pausedTime = 0;
		public static double PausedTime
		{
			get { return _pausedTime; }
			set { _pausedTime = value; }
		}
		//	public static bool PausedTimeDayTime = false;
		private TimeWeatherControlHotbar timeWeatherHotbar;
		public TimeWeatherChanger()
		{
			IsHotbar = true;

			TimePaused = false;
			this._name = "Time Weather Control";
			this._hotbarIcon = new UIImage(UIView.GetEmbeddedTexture("Images/timeRain"));
			this.HotbarIcon.Tooltip = "Change Time/Rain";
			this.HotbarIcon.onLeftClick += HotbarIcon_onLeftClick;

			//timeWeatherHotbar = new TimeWeatherControlHotbar();
			//HEROsMod.ServiceHotbar.AddChild(timeWeatherHotbar);

			timeWeatherHotbar = new TimeWeatherControlHotbar();
			timeWeatherHotbar.HotBarParent = HEROsMod.ServiceHotbar;
			timeWeatherHotbar.Hide();
			this.AddUIView(timeWeatherHotbar);

			Hotbar = timeWeatherHotbar;

			HEROsModNetwork.GeneralMessages.TimePausedOrResumedByServer += GeneralMessages_TimePausedOrResumedByServer;
		}

		void GeneralMessages_TimePausedOrResumedByServer(bool timePaused)
		{
			TimePaused = timePaused;
			timeWeatherHotbar.TimePausedOfResumed();
		}


		void HotbarIcon_onLeftClick(object sender, EventArgs e)
		{
			if (timeWeatherHotbar.selected)
			{
				timeWeatherHotbar.selected = false;
				timeWeatherHotbar.Hide();
			}
			else
			{
				timeWeatherHotbar.selected = true;
				timeWeatherHotbar.Show();
			}

			//timeWeatherHotbar.Visible = !timeWeatherHotbar.Visible;
			//if (timeWeatherHotbar.Visible)
			//{
			//	timeWeatherHotbar.X = this._hotbarIcon.X + this._hotbarIcon.Width / 2 - timeWeatherHotbar.Width / 2;
			//	timeWeatherHotbar.Y = -timeWeatherHotbar.Height;
			//}
		}

		public override void MyGroupUpdated()
		{
			this.HasPermissionToUse = HEROsModNetwork.LoginService.MyGroup.HasPermission("ChangeTimeWeather");
			if (!HasPermissionToUse)
			{
				timeWeatherHotbar.Hide();
			}
			//base.MyGroupUpdated();
		}

		public override void Destroy()
		{
			HEROsModNetwork.GeneralMessages.TimePausedOrResumedByServer -= GeneralMessages_TimePausedOrResumedByServer;
			TimePaused = false;
			HEROsMod.ServiceHotbar.RemoveChild(timeWeatherHotbar);
			base.Destroy();
		}


		public static void ToggleTimePause()
		{
			TimePaused = !TimePaused;
			if (TimePaused)
			{
				PausedTime = Main.time;
			}
		}
		public override void Update()
		{
			if (ModUtils.NetworkMode == NetworkMode.None)
			{
				if (TimePaused)
				{
					Main.time = PausedTime;
				}
			}
			base.Update();
		}
	}

	class TimeWeatherControlHotbar : UIHotbar
	{
		//	static float spacing = 8f;

		public UIImage bPause;
		static Texture2D _playTexture;
		static Texture2D _pauseTexture;
		//static Texture2D _rainTexture;
		//public static Texture2D rainTexture
		//{
		//	get
		//	{
		//		if (_rainTexture == null) _rainTexture = GetEmbeddedTexture("Images/rainIcon");
		//		return _rainTexture;
		//	}
		//}
		public static Texture2D playTexture
		{
			get
			{
				if (_playTexture == null) _playTexture = GetEmbeddedTexture("Images/speed1");
				return _playTexture;
			}
		}
		public static Texture2D pauseTexture
		{
			get
			{
				if (_pauseTexture == null) _pauseTexture = GetEmbeddedTexture("Images/speed0");
				return _pauseTexture;
			}
		}

		public TimeWeatherControlHotbar()
		{
			this.buttonView = new UIView();
			Height = 54;
			UpdateWhenOutOfBounds = true;
			//this.Visible = false;

			this.buttonView.Height = base.Height;
			base.Anchor = AnchorPosition.Top;
			this.AddChild(this.buttonView);

			//UIImage bStopRain = new UIImage(GetEmbeddedTexture("Images/sunIcon"));
			//UIImage bStartRain = new UIImage(rainTexture);
			//bStartRain.Tooltip = "Start Rain";
			//bStopRain.Tooltip = "Stop Rain";
			//bStartRain.onLeftClick += bStartRain_onLeftClick;
			//bStopRain.onLeftClick += bStopRain_onLeftClick;
			//AddChild(bStopRain);
			//AddChild(bStartRain);

			//UIImage nightButton = new UIImage(GetEmbeddedTexture("Images/moonIcon"));
			//nightButton.Tooltip = "Night";
			//nightButton.onLeftClick += nightButton_onLeftClick;
			//UIImage noonButton = new UIImage(GetEmbeddedTexture("Images/sunIcon"));
			//noonButton.Tooltip = "Noon";
			//noonButton.onLeftClick += noonButton_onLeftClick;
			//bPause = new UIImage(pauseTexture);
			//bPause.onLeftClick += bPause_onLeftClick;
			//AddChild(nightButton);
			//AddChild(noonButton);
			//AddChild(bPause);

			//float xPos = spacing;
			//for (int i = 0; i < children.Count; i++)
			//{
			//	if (children[i].Visible)
			//	{
			//		children[i].X = xPos;
			//		xPos += children[i].Width + spacing;
			//		children[i].Y = Height / 2 - children[i].Height / 2;
			//	}
			//}
			//Width = xPos;
		}

		public override void test()
		{
		//	ModUtils.DebugText("TEST " + buttonView.ChildCount);

			Height = 54;
			UpdateWhenOutOfBounds = true;

			UIImage bStopRain = new UIImage(GetEmbeddedTexture("Images/rainStop"));
			UIImage bStartRain = new UIImage(GetEmbeddedTexture("Images/rainIcon"));
			bStartRain.Tooltip = "Start Rain";
			bStopRain.Tooltip = "Stop Rain";
			bStartRain.onLeftClick += bStartRain_onLeftClick;
			bStopRain.onLeftClick += bStopRain_onLeftClick;
			buttonView.AddChild(bStopRain);
			buttonView.AddChild(bStartRain);

			UIImage bStopSandstorm = new UIImage(GetEmbeddedTexture("Images/rainStop"));
			UIImage bStartSandstorm = new UIImage(GetEmbeddedTexture("Images/rainIcon"));
			bStartSandstorm.Tooltip = "Start Sandstorm";
			bStopSandstorm.Tooltip = "Stop Sandstorm";
			bStartSandstorm.onLeftClick += bStartSandstorm_onLeftClick;
			bStopSandstorm.onLeftClick += bStopSandstorm_onLeftClick;
			buttonView.AddChild(bStopSandstorm);
			buttonView.AddChild(bStartSandstorm);

			UIImage nightButton = new UIImage(GetEmbeddedTexture("Images/moonIcon"));
			nightButton.Tooltip = "Night";
			nightButton.onLeftClick += nightButton_onLeftClick;
			UIImage noonButton = new UIImage(GetEmbeddedTexture("Images/sunIcon"));
			noonButton.Tooltip = "Noon";
			noonButton.onLeftClick += noonButton_onLeftClick;
			bPause = new UIImage(TimeWeatherChanger.TimePaused ? playTexture : pauseTexture);
			bPause.onLeftClick += bPause_onLeftClick;
			bPause.Tooltip = TimeWeatherChanger.TimePaused ? "Resume Time" : "Pause Time";// "Toggle Freeze Time";

			buttonView.AddChild(nightButton);
			buttonView.AddChild(noonButton);
			buttonView.AddChild(bPause);

			UIImage sundialButton = new UIImage(GetEmbeddedTexture("Images/timeRain"));
			sundialButton.Tooltip = "Force Enchanted Sundial";
			sundialButton.onLeftClick += sundialButton_onLeftClick;
			buttonView.AddChild(sundialButton);


			//float xPos = spacing;
			//for (int i = 0; i < children.Count; i++)
			//{
			//	if (children[i].Visible)
			//	{
			//		children[i].X = xPos;
			//		xPos += children[i].Width + spacing;
			//		children[i].Y = Height / 2 - children[i].Height / 2;
			//	}
			//}
			//Width = xPos;




			base.CenterXAxisToParentCenter();
			float num = this.spacing;
			for (int i = 0; i < this.buttonView.children.Count; i++)
			{
				this.buttonView.children[i].Anchor = AnchorPosition.Left;
				this.buttonView.children[i].Position = new Vector2(num, 0f);
				this.buttonView.children[i].CenterYAxisToParentCenter();
				this.buttonView.children[i].Visible = true;
				//this.buttonView.children[i].ForegroundColor = buttonUnselectedColor;
				num += this.buttonView.children[i].Width + this.spacing;
			}
			//this.Resize();
			base.Width = num;
			this.buttonView.Width = base.Width;
		}
		public override void Update()
		{
			DoSlideMovement();
			//base.CenterXAxisToParentCenter();
			base.Update();
		}

		//public void Resize()
		//{
		//	float num = this.spacing;
		//	for (int i = 0; i < this.buttonView.children.Count; i++)
		//	{
		//		if (this.buttonView.children[i].Visible)
		//		{
		//			this.buttonView.children[i].X = num;
		//			num += this.buttonView.children[i].Width + this.spacing;
		//		}
		//	}
		//	base.Width = num;
		//	this.buttonView.Width = base.Width;
		//}

		void sundialButton_onLeftClick(object sender, EventArgs e)
		{
			if (Main.netMode == 1) // Client
			{
				HEROsModNetwork.GeneralMessages.RequestForcedSundial();
			}
			else // Single
			{
				Main.fastForwardTime = true;
				Main.sundialCooldown = 0;
				//NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0, 0, 0);
			}

			//if (/*!Main.fastForwardTime &&*/ (Main.netMode == 1 || Main.sundialCooldown == 0))
			//{
			//	if (Main.sundialCooldown == 0)
			//	{
			//		if (Main.netMode == 1)
			//		{
			//			NetMessage.SendData(51, -1, -1, "", Main.myPlayer, 3f, 0f, 0f, 0, 0, 0);
			//			return;
			//		}
			//		Main.fastForwardTime = true;
			//		Main.sundialCooldown = 8;
			//		NetMessage.SendData(7, -1, -1, "", 0, 0f, 0f, 0f, 0, 0, 0);
			//	}
			//}
		}

		void bPause_onLeftClick(object sender, EventArgs e)
		{
			if (Main.netMode != 1)
			{
				TimeWeatherChanger.ToggleTimePause();
				UIImage b = (UIImage)sender;
				TimePausedOfResumed();
				Main.NewText("Time has " + (TimeWeatherChanger.TimePaused ? "been paused" : "resumed"));
			}
			else
			{
				HEROsModNetwork.GeneralMessages.ReqestTimeChange(HEROsModNetwork.GeneralMessages.TimeChangeType.Pause);
			}
		}

		void bStopRain_onLeftClick(object sender, EventArgs e)
		{
			if (Main.netMode == 1)
			{
				HEROsModNetwork.GeneralMessages.RequestStopRain();
				return;
			}
			Main.NewText("Rain has been turned off");

			ModUtils.StopRain();
		}

		void bStartRain_onLeftClick(object sender, EventArgs e)
		{
			if (Main.netMode == 1)
			{
				HEROsModNetwork.GeneralMessages.RequestStartRain();
				return;
			}
			Main.NewText("Rain has been turned on");
			ModUtils.StartRain();
		}

		void bStopSandstorm_onLeftClick(object sender, EventArgs e)
		{
			if (Main.netMode == 1)
			{
				HEROsModNetwork.GeneralMessages.RequestStopSandstorm();
				return;
			}
			Main.NewText("Sandstorm has been turned off");

			ModUtils.StopSandstorm();
		}

		void bStartSandstorm_onLeftClick(object sender, EventArgs e)
		{
			if (Main.netMode == 1)
			{
				HEROsModNetwork.GeneralMessages.RequestStartSandstorm();
				return;
			}
			Main.NewText("Sandstorm has been turned on");
			ModUtils.StartSandstorm();
		}

		public void TimePausedOfResumed()
		{
			if (TimeWeatherChanger.TimePaused)
			{
				bPause.Texture = playTexture;
			}
			else
			{
				bPause.Texture = pauseTexture;
			}
			bPause.Tooltip = TimeWeatherChanger.TimePaused ? "Resume Time" : "Pause Time";
		}

		void nightButton_onLeftClick(object sender, EventArgs e)
		{
			if (Main.netMode != 1)
			{
				Main.dayTime = false;
				Main.time = 0;// 27000.0;
			}
			else
			{
				HEROsModNetwork.GeneralMessages.ReqestTimeChange(HEROsModNetwork.GeneralMessages.TimeChangeType.SetToNight);
			}
		}

		void noonButton_onLeftClick(object sender, EventArgs e)
		{
			if (Main.netMode != 1)
			{
				Main.dayTime = true;
				Main.time = 27000.0;
			}
			else
			{
				HEROsModNetwork.GeneralMessages.ReqestTimeChange(HEROsModNetwork.GeneralMessages.TimeChangeType.SetToNoon);
			}
		}

		void TimeControlWindow_onLeftClick(object sender, EventArgs e)
		{
			UIImage b = (UIImage)sender;
			int rate = (int)b.Tag;
			if (rate > 0)
			{
				//pauseTime = false;
				Main.dayRate = (int)b.Tag;
			}
			else
			{
				//pauseTime = true;
				//previousTime = Main.time;
			}
		}

		//public override void Update()
		//{
		//	if (this.Visible)
		//	{
		//		if (!MouseInside)
		//		{
		//			int mx = Main.mouseX;
		//			int my = Main.mouseY;
		//			float right = DrawPosition.X + Width;
		//			float left = DrawPosition.X;
		//			float top = DrawPosition.Y;
		//			float bottom = DrawPosition.Y + Height;
		//			float dist = 75f;
		//			bool outsideBounds = (mx > right && mx - right > dist) ||
		//								 (mx < left && left - mx > dist) ||
		//								 (my > bottom && my - bottom > dist) ||
		//								 (my < top && top - my > dist);
		//			if ((UIKit.UIView.MouseLeftButton && !MouseInside) || outsideBounds) this.Visible = false;
		//		}
		//	}
		//	base.Update();
		//}

	}
}
