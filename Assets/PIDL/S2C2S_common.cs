﻿ 





// Generated by PIDL compiler.
// Do not modify this file, but modify the source .pidl file.

using System;
namespace S2C2S
{
	internal class Common
	{
		// Message ID that replies to each RMI method. 
			public const Nettention.Proud.RmiID RequestLogin = (Nettention.Proud.RmiID)1000+1;
			public const Nettention.Proud.RmiID NotifyLoginSuccess = (Nettention.Proud.RmiID)1000+2;
			public const Nettention.Proud.RmiID NotifyLoginFailed = (Nettention.Proud.RmiID)1000+3;
			public const Nettention.Proud.RmiID JoinGameRoom = (Nettention.Proud.RmiID)1000+4;
			public const Nettention.Proud.RmiID LeaveGameRoom = (Nettention.Proud.RmiID)1000+5;
			public const Nettention.Proud.RmiID Room_Appear = (Nettention.Proud.RmiID)1000+6;
			public const Nettention.Proud.RmiID Room_Disappear = (Nettention.Proud.RmiID)1000+7;
			public const Nettention.Proud.RmiID GameStart = (Nettention.Proud.RmiID)1000+8;
			public const Nettention.Proud.RmiID PlayerInfo = (Nettention.Proud.RmiID)1000+9;
			public const Nettention.Proud.RmiID Player_Move = (Nettention.Proud.RmiID)1000+10;
			public const Nettention.Proud.RmiID Player_Chat = (Nettention.Proud.RmiID)1000+11;
		// List that has RMI ID.
		public static Nettention.Proud.RmiID[] RmiIDList = new Nettention.Proud.RmiID[] {
			RequestLogin,
			NotifyLoginSuccess,
			NotifyLoginFailed,
			JoinGameRoom,
			LeaveGameRoom,
			Room_Appear,
			Room_Disappear,
			GameStart,
			PlayerInfo,
			Player_Move,
			Player_Chat,
		};
	}
}

				 
