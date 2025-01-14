using Microsoft.EntityFrameworkCore;
using OnlineShop.Api.Data;
using OnlineShop.Shared;
using OnlineShop.Shared.DTOs;
using OnlineShop.Shared.Models;
using OnlineShop.Shared.Services.CartService;

namespace OnlineShop.Api.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<CartItem>>> GetCartItemsAsync(int userId)
        {
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product) 
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return new ServiceResponse<List<CartItem>>
                {
                    Data = null,
                    Success = true,
                    Message = "Koszyk jest pusty."
                };
            }

            return new ServiceResponse<List<CartItem>>
            {
                Data = cartItems,
                Success = true,
                Message = "Produkty w koszyku pobrane pomyślnie."
            };
        }

        public async Task<ServiceResponse<CartItem>> AddToCartAsync(int userId, AddCartItemDto cartItemDto)
        {
            // Znajdź istniejący koszyk użytkownika
            var cartItems = await _context.CartItems
                .Where(ci => ci.UserId == userId)
                .ToListAsync();

            // Sprawdź, czy produkt już istnieje w koszyku
            var existingItem = cartItems.FirstOrDefault(ci => ci.ProductId == cartItemDto.ProductId);

            if (existingItem != null)
            {
                // Produkt już istnieje, więc zwiększamy ilość
                existingItem.Quantity += cartItemDto.Quantity;
                _context.CartItems.Update(existingItem);
            }
            else
            {
                // Produkt nie istnieje, więc dodajemy nowy element do koszyka
                var newItem = new CartItem
                {
                    UserId = userId,
                    ProductId = cartItemDto.ProductId,
                    Quantity = cartItemDto.Quantity
                };
                _context.CartItems.Add(newItem);
            }

            // Zapisz zmiany w bazie danych
            await _context.SaveChangesAsync();

            // Pobierz zaktualizowany element
            var updatedItem = await _context.CartItems
                .FirstAsync(ci => ci.UserId == userId && ci.ProductId == cartItemDto.ProductId);

            return new ServiceResponse<CartItem>
            {
                Data = updatedItem,
                Success = true,
                Message = "Produkt został dodany do koszyka."
            };
        }

        public async Task<ServiceResponse<bool>> RemoveFromCartAsync(int userId, int productId)
        {
            // Znajdź produkt w koszyku użytkownika
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.ProductId == productId);

            if (existingItem == null)
            {
                // Jeśli produkt nie istnieje w koszyku, zwróć odpowiednią wiadomość
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Produkt nie został znaleziony w koszyku użytkownika."
                };
            }

            // Usuń produkt z koszyka
            _context.CartItems.Remove(existingItem);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true,
                Success = true,
                Message = "Produkt został usunięty z koszyka."
            };
        }

        public async Task<ServiceResponse<bool>> ClearCartAsync(int userId)
        {
            var cartItems = await _context.CartItems
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Koszyk jest już pusty lub nie istnieje."
                };
            }

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true,
                Success = true,
                Message = "Koszyk został opróżniony."
            };
        }
    }
}
