

FormManager = {};
self = FormManager;

function FormManager.Init()
    self.m_Forms = {};

end

function FormManager.Show(formName,class)
    local id = Ap.FormManager:Show(formName);
    self.m_Forms[id] = class;
end

function FormManager.ShowDialog(formName,class)
    local id = Ap.FormManager:ShowDialog(formName);
    self.m_Forms[id] = class;
end

-- 界面加载asset开始
function FormManager.FormLoadAssetStart(id,assetName)
    local form = self.m_Forms[id];
    if form ~= nil then
        form:OnLoadAssetStart(asseetName);
    end
end
-- 界面加载asset结束
function FormManager.FormLoadAssetEnd(id,assetName,obj)
    local form = self.m_Forms[id];
    if form ~= nil then
        form:OnLoadAssetEnd(assetName,obj);
    end
end

-- 界面加载
function FormManager.FormLoad(id)
    local form = self.m_Forms[id];
    if form ~= nil then
        form:OnLoad();
    end
end

-- 界面关闭
function FormManager.FormClose(id)
    local form = self.m_Forms[id];
    form:OnClose();
end
