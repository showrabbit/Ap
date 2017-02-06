--region *.lua
--Date
--此文件由[BabeLua]插件自动生成

--endregion

require "Common/Form"

MainForm = class(Form)

function MainForm:ctor()
    self.btnStart = nil;
    self.btnClose = nil;
    self.btnOpen = nil;
end

-- 加载
function MainForm:OnLoad(view)
    self.btnStart = view.Controls[0]:GetComponent("ButtonEx");
    self.btnClose = view.Controls[1]:GetComponent("ButtonEx");
    self.btnOpen = view.Controls[2]:GetComponent("ButtonEx");
end

