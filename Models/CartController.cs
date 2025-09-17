using ASP_NET_hw2.Data;
using ASP_NET_hw2.Models;
using ASP_NET_hw2.Repositories;
using Microsoft.AspNetCore.Mvc;
using ASP_NET_hw2.Helpers;

namespace ASP_NET_hw2.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductRepository _repository;

        public CartController(ApplicationDbContext context)
        {
            _repository = new ProductRepository(context);
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _repository.GetById(id);
            if (product == null) return NotFound();

            var cart = GetCart();
            var existing = cart.FirstOrDefault(c => c.ProductId == id);

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }

            SaveCart(cart);
            return RedirectToAction("Index", "Product");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == id);
            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }
            return RedirectToAction(nameof(Index));
        }

        private List<CartItem> GetCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("Cart");
            return cart ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            HttpContext.Session.SetObjectAsJson("Cart", cart);
        }
    }
}
