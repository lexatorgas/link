using LinkShortener.Data;
using LinkShortener.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkShortener.Services
{
    public class ShortUrlService
    {
        private DataBaseContext dbContext;

        public void DeleteUrl (int id)
        {
            using (dbContext =new DataBaseContext())
            {
                dbContext.Urls.Remove(dbContext.Urls.Where(x => x.Id == id).FirstOrDefault());
                dbContext.SaveChanges();
            }
        }
        public void ChangeUrl(int id, string newLongUrl, string newShortUrl)
        {
            using (dbContext = new DataBaseContext())
            {
                var url = dbContext.Urls.Where(x => x.Id == id).FirstOrDefault();
                url.LongUrl = newLongUrl;
                url.ShortUrl = "http://localhost:5555/go/"+ newShortUrl;
                dbContext.SaveChanges();
            }
        }
        public Url GetUrlById(int id)
        {
            using(dbContext = new DataBaseContext())
            {
                return dbContext.Urls.Where(x => x.Id == id).FirstOrDefault();
            }
        }
        public Url GetUrlByLongUrl(string longUrl)
        {
            using (dbContext = new DataBaseContext())
            {
                return dbContext.Urls.Where(x => x.LongUrl == longUrl).FirstOrDefault();
            }
        }

        public List<Url> GetAllDataBase()
        {
            var list = new List<Url>();
            using (dbContext = new DataBaseContext())
            {                
                list = dbContext.Urls.ToList();
            }
            return list;

        }
        public string LoadLongUrl(string shortUrl)
        {
            using (dbContext = new DataBaseContext())
            {
                var list = dbContext.Urls.ToList();
                var str = list.Where(x => x.ShortUrl == "http://localhost:5555/go/" + shortUrl).ToList();
                if (str.Count==1)
                {
                    str[0].TransitionsCount++;
                    dbContext.SaveChanges();
                    return str[0].LongUrl;
                }
            }
            return null;
        }
        
        public void SaveUrl(string longUrl)
        {
            var returnUrl = new Url();
            returnUrl.LongUrl = longUrl;
            returnUrl.creationData = DateTime.Now.ToString("g");
            returnUrl.ShortUrl = GetShortUrl();
            returnUrl.TransitionsCount = 0;
            using (dbContext = new DataBaseContext())
            {
                dbContext.Database.EnsureCreated();
                dbContext.Urls.Add(returnUrl);
                dbContext.SaveChanges();
            }

        }
        private string GetShortUrl()
        {
            Random rnd = new Random();
            string Alphabet = "0123456789abcdfghjkmnpqrstvwxyzABCDFGHJKLMNPQRSTVWXYZ";
            string str = "";
            for (int i = 0; i < 8; i++)
            {
                str += Alphabet[rnd.Next(Alphabet.Length - 1)];
            }
            return "http://localhost:5555/go/" + str;
        }     
    }
}
