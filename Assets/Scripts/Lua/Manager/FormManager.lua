

FormManager = { };
self = FormManager;
function FormManager.Init()
    -- 窗体
    self.m_Forms = { };
    -- 控制
    self.m_FormCtrs = { };
end
-- 显示
function FormManager.Show(formName, class)
    local id = Managers.F:Show(formName);
    self.m_Forms[id] = class;
    class.m_ID = id;
    return id;
end

-- 关闭界面
function FormManager.Close(id)
    Managers.F:Close(id);
end

-- 绑定控制
function FormManager.BindCtr(id, ctr)

    if self.m_FormCtrs[id] == nil then
        self.m_FormCtrs[id] = { };
    end
    self.m_FormCtrs[id][#self.m_FormCtrs[id] + 1] = ctr;
    -- 为这个view 绑定控制,如果 self.m_Forms里面没有这个view 说明出现错误
    ctr:BindView(self.m_Forms[id]);

end

-- 界面加载asset开始
function FormManager.FormLoadAssetStart(id, assetName)
    local form = self.m_Forms[id];
    if form ~= nil then
        form:OnLoadAssetStart(asseetName);
    end
end
-- 界面加载asset结束
function FormManager.FormLoadAssetEnd(id, assetName, obj)
    local form = self.m_Forms[id];
    if form ~= nil then
        form:OnLoadAssetEnd(assetName, obj);
    end
end

-- 界面加载
function FormManager.FormLoad(id, viewObj)
    local form = self.m_Forms[id];
    if form ~= nil then
        form:OnLoad(viewObj);

        -- lua ctr对象初始化
        for k, v in pairs(self.m_FormCtrs[id]) do
            v:OnLoad();
        end
    end
end

-- 界面获取焦点
function FormManager.FormFouce(id, fouce)

end

-- 界面关闭
function FormManager.FormClose(id)
    
    local form = self.m_Forms[id];
    self.m_FormCtrs[id] = nil;
    self.m_Forms[id] = nil;
end
