using Microsoft.AspNetCore.Mvc;

namespace MvcLab.Controllers
{
    public class LabController : Controller
    {
        public IActionResult Info()
        {
            var labData = new
            {
                Number = 1,
                Topic = "Вступ до ASP.NET Core",
                Goal = "ознайомитися з основними принципами роботи .NET, навчитися налаштовувати середовище розробки та встановлювати необхідні компоненти, набути навичок створення рішень" +
                " та про-ектів різних типів, набути навичок обробки запитів з використанням middleware.",
                Author = "Ліщинський Вадим"
            };

            return View(labData);
        }
    }
}
