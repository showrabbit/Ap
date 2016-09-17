--region *.lua
--Date
--此文件由[BabeLua]插件自动生成



--endregion


require "Common/Class";
require "Common/Define";
require "Common/EventProtocol"
require "Common/FormManager"
require "Common/Functions"
require "Common/NetworkManager";
require "Common/Protocal"

require "View/MainForm"
require "Controller/MainFormCtr"

function Main()
    FormManager.Show("MainForm",MainForm.new(),MainFormCtr.Init());
end