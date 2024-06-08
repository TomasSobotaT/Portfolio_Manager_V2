using PortfolioManager.Base.Entities;
using PortfolioManager.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioManager.Models.Models;
public class Commodity
{
    public int? Id { get; set; }
    public string Name { get; set; }


    public int PriceId { get; set; }

}
