using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace N_tier_solution.Models
{
    public class FileUpload
    {
        public IEnumerable<HttpPostedFileBase> Files { get; set; }
    }
}