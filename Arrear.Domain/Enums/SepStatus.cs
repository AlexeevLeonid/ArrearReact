﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arrear.Domain.Enums
{
    public enum SepStatus
    {
        OrderAssembly,
        FirstWorkshopInWork,
        FirstWorkshopReady,
        SecondWorkshopInWork,
        SecondWorkshopReady,
        Sent
    }
}
