using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using smart_table.Models;
using smart_table.Models.DataBase;
namespace smart_table.Customer.Controllers
{
    public class RecommendationController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly HydrometereologyInterface _hydrometereologyInterface;

        private string _viewsPath = "Customer/Views/Recomendation/";
        private List<Dishes> selectedDishes = new List<Dishes>();
        private List<Dishes> recommendedDishes = new List<Dishes>();

        public RecommendationController(DataBaseContext context)
        {
            _context = context;
            _hydrometereologyInterface = new HydrometereologyInterface();
        }

        public async Task<IActionResult> OpenRecommendationsView()
        {
            ViewData["user_role"] = HttpContext.Session.GetInt32("user_role");
            ViewData["table_code"] = HttpContext.Session.GetString("table_code");
            // https://stackoverflow.com/questions/44063832/what-is-the-best-practice-in-ef-core-for-using-parallel-async-calls-with-an-inje
            // <...> abandoning Dependency Injection
            // which would create development overhead/maintenance debt,
            // potential for bugs if handled wrong,
            // and a departure from Microsoft's official recommendations.

            List<Dishes> discountedDishes = await GetDiscountedDishesTask();
            addToSelectedDishes(discountedDishes);
            List<Dishes> popularDishes = await GetPopularDishesTask();
            addToSelectedDishes(popularDishes);
            List<Dishes> matchingDishes = await GetMatchingDishesTask();
            addToSelectedDishes(matchingDishes);
            List<Dishes> dishesByTemperature = await GetDishesByTemperature();
            addToSelectedDishes(dishesByTemperature);

            var menus = await _context.Menus.Include(m => m.MenuDishes).ToListAsync();
            selectedDishes.ForEach(dish =>
            {
                var menu = menus.Find(m => m.MenuDishes.Any(md => md.FkDishes == dish.Id));
                if (menu.IsActive)
                {
                    addToRecommendations(dish);
                }
            });
            recommendedDishes = recommendedDishes.Distinct().ToList();
            sortRecommendations();

            return View(_viewsPath + "RecommendationsView.cshtml", recommendedDishes);
        }

        private Task<List<Dishes>> GetDiscountedDishesTask()
        {
            return _context.Dishes.Where(d => d.Discount > 0).ToListAsync();
        }

        private Task<List<Dishes>> GetPopularDishesTask()
        {
            return Task.Run(() =>
            {
                var popularDishIds = _context.OrderDishes
                    .GroupBy(od => new { od.FkDishes })
                    .Select(g => new
                    {
                        g.Key.FkDishes,
                        Sum = g.Sum(od => od.Quantity)
                    })
                    .OrderByDescending(od => od.Sum)
                    .Take(2)
                    .ToList()
                    .Select(doc => doc.FkDishes);
                return _context.Dishes.Where(d => popularDishIds.Contains(d.Id)).ToListAsync();
            });
        }

        private Task<List<Dishes>> GetMatchingDishesTask()
        {
            return Task.Run(() =>
            {
                var triedCategories = _context.OrderDishes
                    .Include(od => od.FkDishesNavigation)
                    .Include(od => od.FkDishesNavigation.FkDishCategoriesNavigation)
                    .ToList()
                    .Select(o => o.FkDishesNavigation.FkDishCategories).Distinct().ToList();
                return _context.Dishes
                    .Where(d => !triedCategories.Contains(d.FkDishCategories))
                    .OrderByDescending(d => d.Calories)
                    .Take(2)
                    .ToListAsync();
            });
        }

        private Task<List<Dishes>> GetDishesByTemperature()
        {
            return Task.Run(() =>
            {
                double currentTemp = _hydrometereologyInterface.GetTemperature();
                double minTemp = currentTemp - 5;
                double maxTemp = currentTemp + 5;

                var ordersByTemp = _context.Orders
                    .Where(o => o.Temperature >= minTemp && o.Temperature <= maxTemp)
                    .Select(o => o.Id)
                    .ToList();
                var dishByTempIds = _context.OrderDishes
                    .Include(od => od.FkDishesNavigation)
                    .Where(od => ordersByTemp.Contains(od.FkOrders))
                    .GroupBy(od => new { od.FkDishes })
                    .Select(g => new
                    {
                        g.Key.FkDishes,
                        Sum = g.Sum(od => od.Quantity)
                    })
                    .OrderByDescending(od => od.Sum)
                    .Take(2)
                    .ToList()
                    .Select(doc => doc.FkDishes);
                return _context.Dishes.Where(d => dishByTempIds.Contains(d.Id)).ToListAsync();
            });
        }

        private void addToSelectedDishes(List<Dishes> dishes)
        {
            selectedDishes = selectedDishes.Concat(dishes).ToList();
        }

        private void addToRecommendations(Dishes dish)
        {
            recommendedDishes.Add(dish);
        }

        private void sortRecommendations()
        {
            recommendedDishes = recommendedDishes.OrderByDescending(d => d.Price).ToList();
        }

        // GET: Recomendation/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishes = await _context.Dishes
                .Include(d => d.FkDishCategoriesNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishes == null)
            {
                return NotFound();
            }

            return View(dishes);
        }

        // GET: Recomendation/Create
        public IActionResult Create()
        {
            ViewData["FkDishCategories"] = new SelectList(_context.DishCategories, "Id", "Title");
            return View();
        }

        // POST: Recomendation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,Calories,Discount,FkDishCategories")] Dishes dishes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dishes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkDishCategories"] = new SelectList(_context.DishCategories, "Id", "Title", dishes.FkDishCategories);
            return View(dishes);
        }

        // GET: Recomendation/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishes = await _context.Dishes.FindAsync(id);
            if (dishes == null)
            {
                return NotFound();
            }
            ViewData["FkDishCategories"] = new SelectList(_context.DishCategories, "Id", "Title", dishes.FkDishCategories);
            return View(dishes);
        }

        // POST: Recomendation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Description,Price,Calories,Discount,FkDishCategories")] Dishes dishes)
        {
            if (id != dishes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dishes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishesExists(dishes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkDishCategories"] = new SelectList(_context.DishCategories, "Id", "Title", dishes.FkDishCategories);
            return View(dishes);
        }

        // GET: Recomendation/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishes = await _context.Dishes
                .Include(d => d.FkDishCategoriesNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishes == null)
            {
                return NotFound();
            }

            return View(dishes);
        }

        // POST: Recomendation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var dishes = await _context.Dishes.FindAsync(id);
            _context.Dishes.Remove(dishes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishesExists(long id)
        {
            return _context.Dishes.Any(e => e.Id == id);
        }
    }
}
