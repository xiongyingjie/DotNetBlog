﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBlog.Core.Entity
{
    public class Widget
    {
        public int ID { get; set; }

        public Enums.WidgetType Type { get; set; }

        public string Config { get; set; }
    }
}
