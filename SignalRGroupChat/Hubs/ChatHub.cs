using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRGroupChat.Hubs
{
    public class ChatHub : Hub
    {
        /// <summary>
        /// 指定したグループにメッセージを送信する
        /// </summary>
        /// <param name="group"></param>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessageToGroup(string group, string user, string message)
        {
            await Clients.Group(group).SendAsync("ReceiveMessage", user, message);
        }

        /// <summary>
        /// クライアントをグループに追加する処理
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}