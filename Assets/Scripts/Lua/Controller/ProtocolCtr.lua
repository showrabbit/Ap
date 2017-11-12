-- region *.lua
-- Date
-- 此文件由[BabeLua]插件自动生成

-- 后面这个文件将由工具自动产生
-- 不要手动修改此文件

ProtocolCtr = { };
self = ProtocolCtr;

-- 初始化
function ProtocolCtr.Init()
    local path = Ap.Base.Environment.LuaPath + "/Lua/Model/protocol.pb"
    -- 注册protobuf 协议结构
    protobuf.register_file(path);
end

PROTOCOL_MSG =
{
    [1] = "msg.LOGIN_INFO",                  -- 登陆信息
    [2] = "msg.COMMON_INFO",                 -- 通用信息
    [3] = "msg.PLAYER",                      -- 玩家信息
}

function ProtocolCtr.Parse(key, buffer)
    local data = nil;

    if PROTOCOL_MSG[key] ~= nil then
        data = protobuf.decode(PROTOCOL_MSG[key], buffer);
        EventProtocol.dispatchEvent(PROTOCOL_MSG[key], data);
    else
        --
        print("ProtocolCtr.Parse ERROR " .. key);
    end
end

-- 发送登陆信息
function ProtocolCtr.SendLOGIN_INFO(data)
    local b = ByteBuffer.New();
    b.WriteShort(1);
    b.WriteBytes(protobuf.encode(data));
    Ap.Managers.ManagerManagers.N.SendMessage(b);
end

-- 注册事件
EventProtocol.addEventListener("NetworkManager_OnSocket",
function(data)
    ProtocolCtr.Parse(data.Key, data.Data);
end );

-- endregion
