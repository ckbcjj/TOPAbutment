using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taobao.Top2.Models
{
    public class BaseResult
    {
        public bool Status { get; set; }
        public string ErrorMessage { get; set; }
        public object Result { get; set; }
    }
}