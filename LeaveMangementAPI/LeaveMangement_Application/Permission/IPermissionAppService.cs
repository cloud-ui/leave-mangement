using LeaveMangement_Entity.Dtos.Permission;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.Permission
{
    public interface IPermissionAppService
    {
        object GetMenuTree();
        object GetMenuTreeByPostion(int positionId);
        object SaveSelectMenu(SelectMenuDto selectMenuDto);
    }
}
