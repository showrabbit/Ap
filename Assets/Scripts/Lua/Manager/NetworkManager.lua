
require "Common/define"
require "Common/protocal"
require "Common/functions"

NetworkManager = { };
local this = NetworkManager;


function NetworkManager.Start()
    logWarn("Network.Start!!");
    -- Event.AddListener(Protocal.Connect, this.OnConnect);
    -- Event.AddListener(Protocal.Message, this.OnMessage);
    -- Event.AddListener(Protocal.Exception, this.OnException);
    -- Event.AddListener(Protocal.Disconnect, this.OnDisconnect);
end

-- Socket消息--
function NetworkManager.OnSocket(key, data)
    -- Event.Brocast(tostring(key), data);
    EventProtocol.dispatchEvent("NetworkManager_OnSocket", { Key = key, Data = data });
end

-- 当连接建立时--
function NetworkManager.OnConnect()
    logWarn("Game Server connected!!");
end

-- 异常断线--
function NetworkManager.OnException()
    NetManager:SendConnect();
    logError("OnException------->>>>");
end

-- 连接中断，或者被踢掉--
function NetworkManager.OnDisconnect()
    logError("OnDisconnect------->>>>");
end

-- 读取网络消息--
function NetworkManager.OnMessage(buffer)

    logWarn('OnMessage-------->>>');
end

-- 卸载网络监听--
function NetworkManager.Unload()
    -- Event.RemoveListener(Protocal.Connect);
    -- Event.RemoveListener(Protocal.Message);
    -- Event.RemoveListener(Protocal.Exception);
    -- Event.RemoveListener(Protocal.Disconnect);
    logWarn('Unload Network...');
end