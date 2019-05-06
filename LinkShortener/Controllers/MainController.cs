using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkShortener.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LinkShortener.Controllers
{
    public class MainController : Controller
    {
        private ShortUrlService service = new ShortUrlService();
        public IActionResult General()
        {
            var list = service.GetAllDataBase();
            list.Reverse();
            return View(list);
        }

        public IActionResult Delete(int id)
        {
            service.DeleteUrl(id);
            return RedirectToAction("General");
        }
        public IActionResult GotoCreate()
        {
            return RedirectToAction("Create");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string longUrl)
        {
            var url = service.GetUrlByLongUrl(longUrl);
            if (url != null)
                if (url.LongUrl == longUrl)
                {
                    return RedirectToAction("CantAdd");
                }
            service.SaveUrl(longUrl);
            return RedirectToAction("General");
        }

        [HttpGet("/go/{path:required}")]
        public IActionResult RedirectTo(string path)
        {
            var a = service.LoadLongUrl(path);
            if (a != null)
            {
                return Redirect(a);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var url = service.GetUrlById(id);
            return View(url);
        }
        [HttpPost]
        public IActionResult Edit(string longUrl, string shortUrl, int id)
        {
            var list = service.GetAllDataBase();
            var urlShortRavno = list.Where(x => x.ShortUrl == "http://localhost:5555/go/" + shortUrl).FirstOrDefault();
            var urlLongRavno = list.Where(x => x.LongUrl==longUrl).FirstOrDefault();
            var url = service.GetUrlById(id);
            if(urlShortRavno!=null)
            {
                if(urlShortRavno.LongUrl==url.LongUrl)
                {
                    service.ChangeUrl(id, longUrl, shortUrl);
                    return RedirectToAction("General");
                }
                return RedirectToAction("CantReplace");
            }
              service.ChangeUrl(id, longUrl, shortUrl);
            return RedirectToAction("General");
        }
  
        public IActionResult CantReplace(string longUrl, string shortUrl, int id)
        {
            return View();
        }

        public IActionResult CantAdd(string longUrl)
        {
            return View();
        }

    }
}
