using Microsoft.EntityFrameworkCore;
using Model.Sqlite;
using Service.ShortenUrl.Encrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ShortenUrl
{
    public class ShortenUrlManager : IShortenUrlManager
    {
        protected IEncryptService _encrypt;
        protected DbContext _dbContext;
        protected readonly int HASH_CODE_LENGTH = 8;
        public ShortenUrlManager(IEncryptService encrypt, DbContext dbContext)
        {
            _encrypt = encrypt;
            _dbContext = dbContext;
        }

        public string Shorten(string url)
        {
            var guid = Guid.NewGuid().ToString("N");
            var id = _encrypt.Encode(guid).Substring(0, HASH_CODE_LENGTH);
            _dbContext.Set<ShortUrlTable>().Add(new ShortUrlTable { Id = id, Url = url });
            _dbContext.SaveChanges();
            return id;
        }

        public string Translate(string id)
        {
            var data = _dbContext.Set<ShortUrlTable>()
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (data != null)
                return data.Url;
            else
                return null;
        }
    }
}
