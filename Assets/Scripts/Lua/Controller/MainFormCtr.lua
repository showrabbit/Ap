-- region *.lua
-- Date
-- 此文件由[BabeLua]插件自动生成



-- endregion

MainFormCtr = FormCtr()

-- 初始化函数
function MainFormCtr.Init(id)
    FormManager.BindCtr(id, MainFormCtr.new());
end

-- 加载
function MainFormCtr:OnLoad()
    -- 绑定事件
    LuaPointerClickEvent.Create(m_View.btnStart, function(sender)
        self:btnStart_OnClick(sender);
    end );
end

-- 开始按钮点击
function MainFormCtr:btnStart_OnClick(sender)

end

-- 监听实体数据变更消息
