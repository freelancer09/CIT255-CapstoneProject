using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapstoneProject.Data;
using CapstoneProject.Models;
using Microsoft.CodeAnalysis.CSharp;

namespace CapstoneProject.Controllers
{
    public class GameListController : Controller
    {
        private readonly GameContext _context;

        public GameListController(GameContext context)
        {
            _context = context;
        }

        // GET: GameList
        public async Task<IActionResult> Index(string sortOrder, string search)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CurrentFilter"] = search;

            var gameLists = from l in _context.GameLists select l;

            if (!String.IsNullOrEmpty(search))
            {
                gameLists = gameLists.Where(l => l.Name.Contains(search));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    gameLists = gameLists.OrderByDescending(l => l.Name);
                    break;
                default:
                    gameLists = gameLists.OrderBy(l => l.Name);
                    break;
            }

            return View(await gameLists.ToListAsync());
        }

        // GET: GameList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var gameList = await _context.GameLists.FirstOrDefaultAsync(m => m.ID == id);
            if (gameList == null)
            {
                return NotFound();
            }
            var gameLists = await _context.Games.Where(g => g.GameListID == id).ToListAsync();

            List<GameResult> games = new List<GameResult>();
            foreach (var i in gameLists)
            {
                RAWG rawg = new RAWG();
                GameResult query = new GameResult();
                query = rawg.GetGameData(i.RawgID.ToString());
                games.Add(query);
            }

            var gameListViewModel = new GameListViewModel<Game> { gameList = gameList, gameLists = gameLists, gameResults = games };
            return View(gameListViewModel);
        }

        // GET: GameList/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] GameList gameList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameList);
        }

        // GET: GameList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameList = await _context.GameLists.FindAsync(id);
            if (gameList == null)
            {
                return NotFound();
            }
            return View(gameList);
        }

        // POST: GameList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] GameList gameList)
        {
            if (id != gameList.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameListExists(gameList.ID))
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
            return View(gameList);
        }

        // GET: GameList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameList = await _context.GameLists
                .FirstOrDefaultAsync(m => m.ID == id);
            if (gameList == null)
            {
                return NotFound();
            }

            return View(gameList);
        }

        // POST: GameList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameList = await _context.GameLists.FindAsync(id);
            _context.GameLists.Remove(gameList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameListExists(int id)
        {
            return _context.GameLists.Any(e => e.ID == id);
        }
    }
}
