namespace PortfolioManager.Models.Responses.Metal;

public class MetalPriceApiResponse
{
    public GSJ GSJ { get; set; }
}

public class GSJ
{
    public Gold Gold { get; set; }
    public Silver Silver { get; set; }
}

public class Gold
{
    public USD USD { get; set; }
}

public class Silver
{
    public USD USD { get; set; }
}

public class USD
{
    public decimal? Ask { get; set; }
    public decimal? Bid { get; set; }
}

