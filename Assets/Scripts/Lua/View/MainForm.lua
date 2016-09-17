--region *.lua
--Date
--此文件由[BabeLua]插件自动生成

--endregion

require "../Common/Form"

MainForm = Form()

function MainForm:ctor()
    self.btnStart = nil;
end

-- 加载
function MainForm:OnLoad(view)
    self.btnStart = view.Contorls[0];
end

