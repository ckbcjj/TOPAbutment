using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Tool.PriceTool
{
    public interface IPriceMatch
    {
        string Name { get; set; }

        double GetPrice(double eprice, double commission = 0, int aviebeds = 0, int beds = 0, DateTime sd = default(DateTime), DateTime ed = default(DateTime),
            DateTime bdate = default(DateTime));
    }
}
