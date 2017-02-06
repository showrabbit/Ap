-- region *.lua
-- Date
-- 此文件由[BabeLua]插件自动生成



-- endregion

MainFormCtr = class(FormCtr)

-- 加载
function MainFormCtr:OnLoad()
    -- 绑定事件
    Ap.Lua.PointerClickEvent.Create(self.m_View.btnStart, function(sender)
        self:btnStart_OnClick(sender);
    end );
    Ap.Lua.PointerClickEvent.Create(self.m_View.btnClose, function(sender)
        self:btnClose_OnClick(sender);
    end );
    Ap.Lua.PointerClickEvent.Create(self.m_View.btnOpen, function(sender)
        self:btnOpen_OnClick(sender);
    end );
end

-- 开始按钮点击
function MainFormCtr:btnStart_OnClick(sender)
    print("click" .. self.m_View.m_ID);
end

-- 关闭按钮点击
function MainFormCtr:btnClose_OnClick(sender)
    print("close" .. self.m_View.m_ID);
    FormManager.Close(self.m_View.m_ID);
end

-- 打开新的界面
function MainFormCtr:btnOpen_OnClick(sender)
    local id = FormManager.Show("MainForm", MainForm.new());
    print("open" .. id);
    FormManager.BindCtr(id, MainFormCtr.new());
end

-- 监听实体数据变更消息
