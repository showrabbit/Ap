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
end

-- 开始按钮点击
function MainFormCtr:btnStart_OnClick(sender)
    print("click");
end

-- 监听实体数据变更消息
