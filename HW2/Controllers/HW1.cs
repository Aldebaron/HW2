using Microsoft.AspNetCore.Mvc;


namespace HW2.Controllers;

[ApiController]
[Route("[controller]")]
public class HW1Controller : ControllerBase
{

    private readonly ILogger<HW1Controller> _logger;




    //constructor function
    public HW1Controller(ILogger<HW1Controller> logger, string json = "{}")
    {
        _logger = logger;
    }



    [HttpGet]
    public string Get()
    {
        var list = new List<int>();

        

        int c = 0;
        var t1 = 0;
        var t2 = 0;
        int c1 = 0;

        while (c1 <= 10)
        {
            Random n = new Random();
            int ni = n.Next(100, 200);
            list.Add(ni);
            c1++;

        }
        c1--;
        while (c < c1)
        {
            if (list[c] < list[c + 1])
            {
                c++;
            }

            else if (list[c] > list[c + 1])
            {

                t1 = list[c];
                t2 = list[c + 1];
                list[c] = t2;
                list[c + 1] = t1;
                if (c > 0) { c = c - 1; };
            }
            else if (list[c] == list[c + 1])
            {
                c++;

            }

        }

        var str = String.Join(',', list);
        _logger.LogWarning(String.Join(',', list));
        return str;

    }
}
