﻿using System;
using System.Collections.Generic;

namespace StoreAPI.Models
{
    public partial class Shopcart
    {
        public int CartId { get; set; }
        public int? Uid { get; set; }
        public int? ProId { get; set; }
        public int? Num { get; set; }
    }
}
