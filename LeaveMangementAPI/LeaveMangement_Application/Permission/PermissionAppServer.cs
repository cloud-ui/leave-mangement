using LeaveMangement_Core.Permission;
using LeaveMangement_Entity.Dtos.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.Permission
{
    public class PermissionAppServer : IPermissionAppService
    {
        private PermissionManager _permissionManager = new PermissionManager();
        public object GetMenuTree()
        {
            return _permissionManager.GetMenuTree();
        }
        public object GetMenuTreeByPostion(int positionId)
        {
            return _permissionManager.GetMenuTreeByPostion(positionId);
        }
        public object SaveSelectMenu(SelectMenuDto selectMenuDto)
        {
            return _permissionManager.SaveSelectMenu(selectMenuDto);
        }
    }
}
