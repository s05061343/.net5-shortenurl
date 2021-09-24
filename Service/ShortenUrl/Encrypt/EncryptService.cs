using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Service.ShortenUrl.Encrypt
{
    public class EncryptService : IEncryptService
    {
        protected DbContext _dbContext;
        public EncryptService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string Decode(string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }

        public string Encode(string str)
        {
            var result = Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
            return result;
        }
    }
}
