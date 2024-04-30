using Microsoft.AspNetCore.Mvc;
namespace mvc;

public class HomeController : Controller{
    private readonly IDataContext dataContext;
    public HomeController(IDataContext context){
        dataContext = context;
    }

    public async Task<IActionResult> Index(){
        List<Producto> productos = await dataContext.obtenerProductosAsync();   
        return View(productos);
    }
    public IActionResult Error(){
        return View();
    }
}