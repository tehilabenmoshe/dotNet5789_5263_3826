﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
public interface IBL
{
    IBoOrder Order { get; }
    IBoProduct Product { get; }
    IBoCart cart { get; }
}
