using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class MyController : ControllerBase
{
    [HttpGet]
    public int GetNumber(){
        return 4;
    }

    [HttpPost]
    public int PostNumber([FromBody]int number){
        return number * 2;
    }
}