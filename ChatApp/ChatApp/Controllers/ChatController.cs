using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private static Dictionary<string, string> messages = new Dictionary<string, string>();

        [HttpGet]
        public IActionResult Show()
        {
            if (messages.Count() < 1)
            {
                return View(new ChatViewModel());
            }

            var chat = new ChatViewModel()
            {
                Messages = messages
                .Select(m => new MessageViewModel()
                {
                    Sender = m.Key,
                    Message = m.Value
                })
                .ToList()
            };

            return View(chat);

        }

        [HttpPost]
        public IActionResult Send(ChatViewModel chat)
        {
            var newMessage = chat.CurrentMessage;

            messages.Add(newMessage.Sender, newMessage.Message);

            return RedirectToAction("Show");
        }
    }
}
