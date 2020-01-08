#pragma once

#if defined(__ORBIS__)
#include <kernel.h>

#include <assert.h>
#define Assert assert

#include "PlayerInterface/UnityPrxPlugin.h"
#include "PlayerInterface/UnityEventQueue.h"
#include <system_service.h>
#endif //defined(__ORBIS__)


#include <string>

#include "ProudNetClient.h"
#include "ThreadPool.h"

#if defined(_WIN32)
#define PNAPI __stdcall

#pragma comment(lib, "Advapi32.lib")
#pragma comment(lib, "Shell32.lib")

#else
#define PNAPI
#endif

typedef void (PNAPI  *CallbackJoinServerComplete)(void* charpHandle, void* info, void* replyFromServer);
typedef void (PNAPI *CallbackLeaveServer)(void* charpHandle, void *errorinfo);
typedef void (PNAPI *CallbackP2PMemberJoin)(void* charpHandle, int memberHostID, int groupHostID, int memberCount, void* message);
typedef void (PNAPI *CallbackP2PMemberLeave)(void* charpHandle, int memberHostID, int groupHostID, int memberCount);
typedef void (PNAPI *CallbackChangeP2PRelayState)(void* charpHandle, int remoteHostID, int reason);
typedef void (PNAPI *CallbackChangeServerUdpState)(void* charpHandle, int reason);
typedef void (PNAPI *CallbackSynchronizeServerTime)(void* charpHandle);

typedef void (PNAPI *CallbackError)(void* charpHandle, void* errorInfo);
typedef void (PNAPI *CallbackWarning)(void* charpHandle, void* errorInfo);
typedef void (PNAPI *CallbackInformation)(void* charpHandle, void* errorInfo);
typedef void (PNAPI *CallbackException)(void* charpHandle, int remote, void* what);

typedef void (PNAPI *CallbackServerOffline)(void* charpHandle, void* args);
typedef void (PNAPI *CallbackServerOnline)(void* charpHandle, void* args);
typedef void (PNAPI *CallbackP2PMemberOffline)(void* charpHandle, void* args);
typedef void (PNAPI *CallbackP2PMemberOnline)(void* charpHandle, void* args);

typedef void (PNAPI *CallbackNoRmiProcessed)(void* charpHandle, int rmiID);
typedef void (PNAPI *CallbackReceiveUserMessage)(void* charpHandle, int sender, void* rmiContext, void* payload, int payloadLength);

//////////////////////////////////////////////////////////////////////////

typedef void* (PNAPI *CallbackGetRmiIDList)(void* charpHandle);
typedef int (PNAPI *CallbackGetRmiIDListCount)(void* charpHandle);
typedef bool (PNAPI *CallbackProcessReceivedMessage)(void* charpHandle, void* pa, int64_t hostTag);