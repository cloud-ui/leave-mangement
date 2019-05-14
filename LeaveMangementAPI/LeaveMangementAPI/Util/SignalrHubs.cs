using LeaveMangement_Application.Approval;
using LeaveMangement_Application.Common;
using LeaveMangementAPI.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMangementAPI.Util
{
    public class SignalrHubs : Hub, ISignalrHubs
    {
        static readonly Dictionary<string, string> Users = new Dictionary<string, string>();
        private readonly IHubContext<SignalrHubs> hubContext;
        private readonly IApprovalAppService approvalAppService = new ApprovalAppService();
        private readonly ICommonAppService commonAppService = new CommonAppService();

        public SignalrHubs(IHubContext<SignalrHubs> hubContext)
        {
            this.hubContext = hubContext;
        }
        public async void SendMessage(string user)
        {
            Users[user] = Context.ConnectionId;
            string account = commonAppService.GetUserAccount(int.Parse(user));
            if(approvalAppService.GetInform(account).Count != 0)
            {
                await hubContext.Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "您有新的消息");
            }
        }
        ///// <summary>
        ///// 建立连接时触发
        ///// </summary>
        ///// <returns></returns>
        //public override async Task OnConnectedAsync()
        //{
        //    //await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} joined");
        //}

        ///// <summary>
        ///// 离开连接时触发
        ///// </summary>
        ///// <param name="ex"></param>
        ///// <returns></returns>
        //public override async Task OnDisconnectedAsync(Exception ex)
        //{
        //    //await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} left");
        //}

        /// <summary>
        /// 向指定Id推送消息
        /// </summary>
        /// <param name="id">要推送消息的对象</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task Send(string id, string message)
        {
            if (Users.ContainsKey(id))
            {
                string connectId = Users[id];
                await hubContext.Clients.Client(connectId).SendAsync("ReceiveMessage", message);
            }
        }
        ///// <summary>
        ///// 向所有人推送消息
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="message"></param>
        ///// <returns></returns>
        //public async Task SendMessage()
        //{

        //    await hubContext.Clients.All.SendAsync("ReceiveMessage", "");

        //}
    }
}
