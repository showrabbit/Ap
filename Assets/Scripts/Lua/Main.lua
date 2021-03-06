-- region *.lua
-- Date
-- 此文件由[BabeLua]插件自动生成



-- endregion

require "Common/Class";
require "Common/Define";
require "Common/EventProtocol"
require "Common/Functions"
require "Common/Protocal"
require "Manager/FormManager"
require "Manager/NetworkManager";
require "Common/Form";
require "Common/FormCtr"


require "View/MainForm"
require "Controller/MainFormCtr"
require "Controller/ProtocolCtr"

function Main()
    FormManager.Init();

    local id = FormManager.Show("MainForm", MainForm.new());
    FormManager.BindCtr(id, MainFormCtr.new());
    
end