using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcMusicStore.Infrastructure;
using MvcMusicStore.Models;
using NLog;
using PerformanceCounterHelper;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();
        private readonly NLog.ILogger logger;

        public HomeController(ILogger logger)
        {
            this.logger = logger;
        }

        // GET: /Home/
        public async Task<ActionResult> Index()
        {
            logger.Debug("Enter to home page");

            return View(await _storeContext.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(6)
                .ToListAsync());
        }

        // GET: /Home/Report
        public ActionResult Report()
        {
            ReportGenerator rg = new ReportGenerator();
            rg.GenerateScript();
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _storeContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}