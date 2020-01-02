﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PPSL.Web.Models;

namespace PPSL.Web.Controllers
{
  public class MovieController : Controller
  {
    //依赖注入
    private readonly MvcMovieContext _context;

    public MovieController(MvcMovieContext context)
    {
      _context = context;
    }

    // GET: Movie
    public async Task<IActionResult> Index(string movieGenre, string searchString)
    {
      IQueryable<string> genreQuery = from m in _context.Movie
                                      orderby m.Genre
                                      select m.Genre;

      var movies = from m in _context.Movie select m;
      if (!String.IsNullOrEmpty(searchString))
      {
        movies = movies.Where(s => s.Title.Contains(searchString));
      }
      if (!string.IsNullOrEmpty(movieGenre))
      {
        movies = movies.Where(s => s.Genre == movieGenre);
      }
      var movieGenreVM = new MovieGenreViewModel
      {
        Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
        Movies = await movies.ToListAsync()
      };
      return View(movieGenreVM);
    }

    [HttpPost]
    public string Index(string searchString, bool notUsed)
    {
      return "From [HttpPost]Index: filter on " + searchString;
    }

    // GET: Movie/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var movie = await _context.Movie
          .FirstOrDefaultAsync(m => m.Id == id);
      if (movie == null)
      {
        return NotFound();
      }

      return View(movie);
    }

    // GET: Movie/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Movie/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
    {
      if (ModelState.IsValid)
      {
        _context.Add(movie);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(movie);
    }

    // GET: Movie/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var movie = await _context.Movie.FindAsync(id);
      if (movie == null)
      {
        return NotFound();
      }
      return View(movie);
    }

    /// <summary>
    /// 电影修改
    /// ValidateAntiForgeryToken 防止请求伪造
    /// </summary>
    /// <param name="id">电影ID</param>
    /// <param name="movie">电影实体</param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
    {
      if (id != movie.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(movie);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!MovieExists(movie.Id))
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
      return View(movie);
    }

    // GET: Movie/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var movie = await _context.Movie
          .FirstOrDefaultAsync(m => m.Id == id);
      if (movie == null)
      {
        return NotFound();
      }

      return View(movie);
    }

    // POST: Movie/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var movie = await _context.Movie.FindAsync(id);
      _context.Movie.Remove(movie);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int id)
    {
      return _context.Movie.Any(e => e.Id == id);
    }
  }
}
