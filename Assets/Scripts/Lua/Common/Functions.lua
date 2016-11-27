
--输出日志--
function log(str)
    
    Ap.Tools.Logger.Instance:Write(0,str);
end

--错误日志--
function logError(str) 
	Ap.Tools.Logger.Instance:Write(2,str);
end

--警告日志--
function logWarn(str) 
	Ap.Tools.Logger.Instance:Write(1,str);
end

--查找对象--
function find(str)
	return GameObject.Find(str);
end

function destroy(obj)
	GameObject.Destroy(obj);
end

function newObject(prefab)
	return GameObject.Instantiate(prefab);
end


function child(str)
	return transform:FindChild(str);
end

function subGet(childNode, typeName)		
	return child(childNode):GetComponent(typeName);
end
