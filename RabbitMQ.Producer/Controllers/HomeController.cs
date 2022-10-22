using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Producer.Models;
using System.Diagnostics;
using RabbitMQ.Domain.Models;
using RabbitMQ.Domain.ViewModel;
using RabbitMQ.Helper.Abstraction;

namespace RabbitMQ.Producer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IProducer _producer;
        private readonly IRabbitApi _api;

        public HomeController(ILogger<HomeController> logger, IProducer producer, IRabbitApi api)
        {
            _logger = logger;
            _producer = producer;
            _api = api;
        }

        public IActionResult Index()
        {
            var model = new HomeVM()
            {
                Queues = _api.ListQueues().Result,
                Exchanges = _api.ListExchanges().Result
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateQueue(Queue model)
        {
            var result = _producer.CreateQueue(model);
            var list = await _api.ListQueues();
            return PartialView("Partials/_QueueList", list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateExchange(Exchange model)
        {
            var result = _producer.CreateExchange(model);
            var list = await _api.ListExchanges();
            return PartialView("Partials/_ExchangeList", list);
        }

        [HttpGet]
        public async Task<IActionResult> ExchangeList()
        {
            var list = await _api.ListExchanges();
            return Json(list);
        }

        [HttpGet]
        public IActionResult BindQueue(string queueName)
        {
            return PartialView("Partials/_QueueBind", new QueueBind(queueName));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BindQueue(QueueBind model)
        {
            var result = _producer.BindQueue(model);

            return Json(result);
        }

        [HttpGet]
        public IActionResult Message()
        {
            return PartialView("Partials/_Message", new Message());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Message(Message model)
        {
            var result = _producer.PublishMessage(model);

            return Json(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}