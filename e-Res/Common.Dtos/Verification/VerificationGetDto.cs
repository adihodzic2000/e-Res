using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Verification
{
    public class VerificationGetDto
    {
        public string Code { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
