-- �¼�������
EventProtocol = {}

EventProtocol._listeners = {}
EventProtocol._nextListenerHandleIndex = 0;

-- ����¼�����
function EventProtocol.addEventListener(eventName, listener)
    eventName = string.upper(eventName)
    if EventProtocol._listeners[eventName] == nil then
        EventProtocol._listeners[eventName] = {}
    end

    EventProtocol._nextListenerHandleIndex = EventProtocol._nextListenerHandleIndex + 1
    local handle = tostring(EventProtocol._nextListenerHandleIndex)
    EventProtocol._listeners[eventName][handle] = listener
end

-- �ַ��¼�
function EventProtocol.dispatchEvent(eventName, value)
		eventName = string.upper(eventName)
    if EventProtocol._listeners[eventName] == nil then 
    	return 
    end
    for handle, listener in pairs(EventProtocol._listeners[eventName]) do
        listener(value)
    end
end

-- �Ƴ�����
function EventProtocol.removeEventListenersByEvent(eventName)
    EventProtocol._listeners[string.upper(eventName)] = nil
end

-- �Ƴ����м���
function EventProtocol:removeAllEventListeners()
    EventProtocol._listeners = {}
end

-- ����Ƿ�������¼��ļ�����
function EventProtocol:hasEventListener(eventName)
    eventName = string.upper(tostring(eventName))
    local t = EventProtocol._listeners[eventName]
    if not t then
        return false
    end
    for _, __ in pairs(t) do
        return true
    end
    return false
end

-- ��ӡ���м���
function EventProtocol.dumpAllEventListeners()
    log("---- EventProtocol:dumpAllEventListeners() ----")
    for name, listeners in pairs(EventProtocol._listeners) do
        log("-- event: %s".. name)
        for handle, listener in pairs(listeners) do
            log("--     listener: "..tostring(listener)..", handle: "..tostring(handle))
        end
    end
end
