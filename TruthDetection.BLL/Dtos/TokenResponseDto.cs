using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TruthDetection.BLL.Dtos
{
    public class TokenResponseDto
    {
        public string token { get; set; }
        public DateTime exp { get; set; }


    }
}
