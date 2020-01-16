﻿




// Generated by PIDL compiler.
// Do not modify this file, but modify the source .pidl file.

using System;
using System.Net;	     

namespace S2C2S
{
	internal class Stub:Nettention.Proud.RmiStub
	{
public AfterRmiInvocationDelegate AfterRmiInvocation = delegate(Nettention.Proud.AfterRmiSummary summary) {};
public BeforeRmiInvocationDelegate BeforeRmiInvocation = delegate(Nettention.Proud.BeforeRmiSummary summary) {};

		public delegate bool RequestLoginDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, System.String id, System.String password);  
		public RequestLoginDelegate RequestLogin = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, System.String id, System.String password)
		{ 
			return false;
		};
		public delegate bool NotifyLoginSuccessDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext);  
		public NotifyLoginSuccessDelegate NotifyLoginSuccess = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext)
		{ 
			return false;
		};
		public delegate bool NotifyLoginFailedDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, System.String reason);  
		public NotifyLoginFailedDelegate NotifyLoginFailed = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, System.String reason)
		{ 
			return false;
		};
		public delegate bool JoinGameRoomDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int character_num);  
		public JoinGameRoomDelegate JoinGameRoom = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int character_num)
		{ 
			return false;
		};
		public delegate bool LeaveGameRoomDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext);  
		public LeaveGameRoomDelegate LeaveGameRoom = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext)
		{ 
			return false;
		};
		public delegate bool Room_AppearDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int hostID, System.String id, int character_num, System.String team_color, int team_num);  
		public Room_AppearDelegate Room_Appear = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int hostID, System.String id, int character_num, System.String team_color, int team_num)
		{ 
			return false;
		};
		public delegate bool Room_DisappearDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int team_num);  
		public Room_DisappearDelegate Room_Disappear = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int team_num)
		{ 
			return false;
		};
		public delegate bool GameStartDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext);  
		public GameStartDelegate GameStart = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext)
		{ 
			return false;
		};
		public delegate bool PlayerInfoDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int team_num, int character_num, float px, float py, float pz, float rx, float ry, float rz);  
		public PlayerInfoDelegate PlayerInfo = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int team_num, int character_num, float px, float py, float pz, float rx, float ry, float rz)
		{ 
			return false;
		};
	public override bool ProcessReceivedMessage(Nettention.Proud.ReceivedMessage pa, Object hostTag) 
	{
		Nettention.Proud.HostID remote=pa.RemoteHostID;
		if(remote==Nettention.Proud.HostID.HostID_None)
		{
			ShowUnknownHostIDWarning(remote);
		}

		Nettention.Proud.Message __msg=pa.ReadOnlyMessage;
		int orgReadOffset = __msg.ReadOffset;
        Nettention.Proud.RmiID __rmiID = Nettention.Proud.RmiID.RmiID_None;
        if (!__msg.Read( out __rmiID))
            goto __fail;
					
		switch(__rmiID)
		{
        case Common.RequestLogin:
            ProcessReceivedMessage_RequestLogin(__msg, pa, hostTag, remote);
            break;
        case Common.NotifyLoginSuccess:
            ProcessReceivedMessage_NotifyLoginSuccess(__msg, pa, hostTag, remote);
            break;
        case Common.NotifyLoginFailed:
            ProcessReceivedMessage_NotifyLoginFailed(__msg, pa, hostTag, remote);
            break;
        case Common.JoinGameRoom:
            ProcessReceivedMessage_JoinGameRoom(__msg, pa, hostTag, remote);
            break;
        case Common.LeaveGameRoom:
            ProcessReceivedMessage_LeaveGameRoom(__msg, pa, hostTag, remote);
            break;
        case Common.Room_Appear:
            ProcessReceivedMessage_Room_Appear(__msg, pa, hostTag, remote);
            break;
        case Common.Room_Disappear:
            ProcessReceivedMessage_Room_Disappear(__msg, pa, hostTag, remote);
            break;
        case Common.GameStart:
            ProcessReceivedMessage_GameStart(__msg, pa, hostTag, remote);
            break;
        case Common.PlayerInfo:
            ProcessReceivedMessage_PlayerInfo(__msg, pa, hostTag, remote);
            break;
		default:
			 goto __fail;
		}
		return true;
__fail:
	  {
			__msg.ReadOffset = orgReadOffset;
			return false;
	  }
	}
    void ProcessReceivedMessage_RequestLogin(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        System.String id; Nettention.Proud.Marshaler.Read(__msg,out id);	
System.String password; Nettention.Proud.Marshaler.Read(__msg,out password);	
core.PostCheckReadMessage(__msg, RmiName_RequestLogin);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=id.ToString()+",";
parameterString+=password.ToString()+",";
        NotifyCallFromStub(Common.RequestLogin, RmiName_RequestLogin,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.RequestLogin;
        summary.rmiName = RmiName_RequestLogin;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =RequestLogin (remote,ctx , id, password );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_RequestLogin);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.RequestLogin;
        summary.rmiName = RmiName_RequestLogin;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_NotifyLoginSuccess(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        core.PostCheckReadMessage(__msg, RmiName_NotifyLoginSuccess);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
                NotifyCallFromStub(Common.NotifyLoginSuccess, RmiName_NotifyLoginSuccess,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.NotifyLoginSuccess;
        summary.rmiName = RmiName_NotifyLoginSuccess;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =NotifyLoginSuccess (remote,ctx  );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_NotifyLoginSuccess);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.NotifyLoginSuccess;
        summary.rmiName = RmiName_NotifyLoginSuccess;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_NotifyLoginFailed(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        System.String reason; Nettention.Proud.Marshaler.Read(__msg,out reason);	
core.PostCheckReadMessage(__msg, RmiName_NotifyLoginFailed);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=reason.ToString()+",";
        NotifyCallFromStub(Common.NotifyLoginFailed, RmiName_NotifyLoginFailed,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.NotifyLoginFailed;
        summary.rmiName = RmiName_NotifyLoginFailed;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =NotifyLoginFailed (remote,ctx , reason );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_NotifyLoginFailed);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.NotifyLoginFailed;
        summary.rmiName = RmiName_NotifyLoginFailed;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_JoinGameRoom(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int character_num; Nettention.Proud.Marshaler.Read(__msg,out character_num);	
core.PostCheckReadMessage(__msg, RmiName_JoinGameRoom);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=character_num.ToString()+",";
        NotifyCallFromStub(Common.JoinGameRoom, RmiName_JoinGameRoom,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.JoinGameRoom;
        summary.rmiName = RmiName_JoinGameRoom;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =JoinGameRoom (remote,ctx , character_num );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_JoinGameRoom);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.JoinGameRoom;
        summary.rmiName = RmiName_JoinGameRoom;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_LeaveGameRoom(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        core.PostCheckReadMessage(__msg, RmiName_LeaveGameRoom);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
                NotifyCallFromStub(Common.LeaveGameRoom, RmiName_LeaveGameRoom,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.LeaveGameRoom;
        summary.rmiName = RmiName_LeaveGameRoom;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =LeaveGameRoom (remote,ctx  );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_LeaveGameRoom);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.LeaveGameRoom;
        summary.rmiName = RmiName_LeaveGameRoom;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_Room_Appear(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int hostID; Nettention.Proud.Marshaler.Read(__msg,out hostID);	
System.String id; Nettention.Proud.Marshaler.Read(__msg,out id);	
int character_num; Nettention.Proud.Marshaler.Read(__msg,out character_num);	
System.String team_color; Nettention.Proud.Marshaler.Read(__msg,out team_color);	
int team_num; Nettention.Proud.Marshaler.Read(__msg,out team_num);	
core.PostCheckReadMessage(__msg, RmiName_Room_Appear);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=hostID.ToString()+",";
parameterString+=id.ToString()+",";
parameterString+=character_num.ToString()+",";
parameterString+=team_color.ToString()+",";
parameterString+=team_num.ToString()+",";
        NotifyCallFromStub(Common.Room_Appear, RmiName_Room_Appear,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.Room_Appear;
        summary.rmiName = RmiName_Room_Appear;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =Room_Appear (remote,ctx , hostID, id, character_num, team_color, team_num );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_Room_Appear);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.Room_Appear;
        summary.rmiName = RmiName_Room_Appear;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_Room_Disappear(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int team_num; Nettention.Proud.Marshaler.Read(__msg,out team_num);	
core.PostCheckReadMessage(__msg, RmiName_Room_Disappear);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=team_num.ToString()+",";
        NotifyCallFromStub(Common.Room_Disappear, RmiName_Room_Disappear,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.Room_Disappear;
        summary.rmiName = RmiName_Room_Disappear;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =Room_Disappear (remote,ctx , team_num );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_Room_Disappear);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.Room_Disappear;
        summary.rmiName = RmiName_Room_Disappear;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_GameStart(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        core.PostCheckReadMessage(__msg, RmiName_GameStart);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
                NotifyCallFromStub(Common.GameStart, RmiName_GameStart,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.GameStart;
        summary.rmiName = RmiName_GameStart;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =GameStart (remote,ctx  );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_GameStart);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.GameStart;
        summary.rmiName = RmiName_GameStart;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_PlayerInfo(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        int team_num; Nettention.Proud.Marshaler.Read(__msg,out team_num);	
int character_num; Nettention.Proud.Marshaler.Read(__msg,out character_num);	
float px; Nettention.Proud.Marshaler.Read(__msg,out px);	
float py; Nettention.Proud.Marshaler.Read(__msg,out py);	
float pz; Nettention.Proud.Marshaler.Read(__msg,out pz);	
float rx; Nettention.Proud.Marshaler.Read(__msg,out rx);	
float ry; Nettention.Proud.Marshaler.Read(__msg,out ry);	
float rz; Nettention.Proud.Marshaler.Read(__msg,out rz);	
core.PostCheckReadMessage(__msg, RmiName_PlayerInfo);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=team_num.ToString()+",";
parameterString+=character_num.ToString()+",";
parameterString+=px.ToString()+",";
parameterString+=py.ToString()+",";
parameterString+=pz.ToString()+",";
parameterString+=rx.ToString()+",";
parameterString+=ry.ToString()+",";
parameterString+=rz.ToString()+",";
        NotifyCallFromStub(Common.PlayerInfo, RmiName_PlayerInfo,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.PlayerInfo;
        summary.rmiName = RmiName_PlayerInfo;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =PlayerInfo (remote,ctx , team_num, character_num, px, py, pz, rx, ry, rz );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_PlayerInfo);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.PlayerInfo;
        summary.rmiName = RmiName_PlayerInfo;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
#if USE_RMI_NAME_STRING
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_RequestLogin="RequestLogin";
public const string RmiName_NotifyLoginSuccess="NotifyLoginSuccess";
public const string RmiName_NotifyLoginFailed="NotifyLoginFailed";
public const string RmiName_JoinGameRoom="JoinGameRoom";
public const string RmiName_LeaveGameRoom="LeaveGameRoom";
public const string RmiName_Room_Appear="Room_Appear";
public const string RmiName_Room_Disappear="Room_Disappear";
public const string RmiName_GameStart="GameStart";
public const string RmiName_PlayerInfo="PlayerInfo";
       
public const string RmiName_First = RmiName_RequestLogin;
#else
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_RequestLogin="";
public const string RmiName_NotifyLoginSuccess="";
public const string RmiName_NotifyLoginFailed="";
public const string RmiName_JoinGameRoom="";
public const string RmiName_LeaveGameRoom="";
public const string RmiName_Room_Appear="";
public const string RmiName_Room_Disappear="";
public const string RmiName_GameStart="";
public const string RmiName_PlayerInfo="";
       
public const string RmiName_First = "";
#endif
		public override Nettention.Proud.RmiID[] GetRmiIDList { get{return Common.RmiIDList;} }
		
	}
}

