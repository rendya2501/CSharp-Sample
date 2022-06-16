using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SignalRGroupChat.Pages
{
    public class RoomModel : PageModel
    {
        public string RoomName;

        public void OnGet(string roomName)
        {
            RoomName = roomName;
        }
    }
}
