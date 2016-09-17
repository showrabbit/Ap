--region *.lua
--Date
--此文件由[BabeLua]插件自动生成



--endregion

Form = class()

function Form:ctor()
    self.m_View = nil;
    self.m_ID = -1;
end

function Form:OnLoad(viewObj)
    self.m_View = viewObj;
end

-- 加载资源
function Form:OnLoadAssetStart(assetName)

end

-- 加载资源结束
function Form:OnLoadAssetEnd(assetName,obj)
    
end