using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMangementAPI.Util
{
    public class SignalrHubs : Hub
    {
        /// <summary>
        /// 建立连接时触发
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {

            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} joined");
        }

        /// <summary>
        /// 离开连接时触发
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception ex)
        {

            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} left");
        }
        /// <summary>
        /// 向指定Id推送消息
        /// </summary>
        /// <param name="userid">要推送消息的对象</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task Echo(string userid, string message)  //Echo：前端请求的方法名字
        {
            return Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", $"{Context.ConnectionId}: {message}");  //ReceiveMessage：前端页面的方法名字，$：返回的内容
        }
        /// <summary>
        /// 向所有人推送消息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage()
        {

            await Clients.All.SendAsync("ReceiveMessage", "");

        }
    }
}
