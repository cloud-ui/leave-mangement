using LeaveMangement_Entity.Dtos.Permission;
using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaveMangement_Core.Permission
{
    public class PermissionManager
    {
        private KaoQinContext _ctx = new KaoQinContext();
        public List<AllMenuDto> GetMenuTree(int parentId = 0)
        {
            List<AllMenuDto> allMenuDtos = (from menu in _ctx.Menu
                                            where menu.ParentId == parentId
                                            select new AllMenuDto
                                            {
                                                Id = menu.Id,
                                                Label = menu.Name,
                                                PositionId = menu.PositionId,
                                            }).ToList();
            List<AllMenuDto> menus = new List<AllMenuDto>();
            foreach(AllMenuDto item in allMenuDtos)
            {
                AllMenuDto menu = new AllMenuDto()
                {
                    Id = item.Id,
                    Label = item.Label,
                    PositionId = item.PositionId,
                    Children = GetMenuTree(item.Id)
                };
                menus.Add(menu);
            }
            return menus;
        }
        public List<AllMenuDto> GetMenuTreeByPostion(int positionId,int parentId = 0)
        {
            //找出根目录
            List<AllMenuDto> parents = (from menu in _ctx.Menu
                                   where menu.ParentId == parentId
                                   select new AllMenuDto
                                   {
                                       Id = menu.Id,
                                       Label = menu.Name,
                                       PositionId = menu.PositionId,
                                   }).ToList();
            List<AllMenuDto> menus = new List<AllMenuDto>();
            foreach (AllMenuDto menu in parents)
            {
                int[] positionIds = StringToInt(menu.PositionId);
                if (Array.IndexOf(positionIds, positionId) != -1)   //-1不存在
                {
                    AllMenuDto menuNode = new AllMenuDto()
                    {
                        Id = menu.Id,
                        Label = menu.Label,
                        PositionId = menu.PositionId,
                        Children = GetMenuTreeByPostion( positionId, menu.Id)
                    };
                    menus.Add(menuNode);
                }
            };
            return menus;
        }
        public object SaveSelectMenu(SelectMenuDto selectMenuDto)
        {
            var result = new object();
            try
            {
                List<Menu> menus = _ctx.Menu.Where(m => selectMenuDto.MenusId.Contains(m.Id)).ToList();
                foreach (Menu item in menus)
                {
                    int[] positionIds = StringToInt(item.PositionId);
                    if (Array.IndexOf(positionIds, selectMenuDto.PositionId) == -1)
                    {
                        string str = item.PositionId + "," + selectMenuDto.PositionId;
                        item.PositionId = str;
                        _ctx.SaveChanges();
                    }
                }
                
                result = new
                {
                    isSuccess = true,
                    message = "权限配置成功！"
                };
            }
            catch
            {
                result = new
                {
                    isSuccess = false,
                    message = "权限配置失败！"
                };
            }
            return result;
        }
        private int[] StringToInt(string positionId)
        {
            string[] positionIdStr = positionId.Split(',');
            int[] positionIds = new int[positionIdStr.Length];
            for (int i = 0; i < positionIdStr.Length; i++)
            {
                positionIds[i] = int.Parse(positionIdStr[i]);
            }
            return positionIds;
        }
    }
}
