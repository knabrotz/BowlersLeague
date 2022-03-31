using BowlersLeague.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlersLeague.Controllers
{
    public class HomeController : Controller
    {
        private IBowlersRepository _repo { get; set; }

        public HomeController(IBowlersRepository temp)
        {
            _repo = temp;   
        }

        public IActionResult Index(int? teamId, string teamName)
        {
            //update teams info for the view
            ViewBag.Teams = _repo.Teams.ToList();
            ViewBag.SelectedTeam = teamId;
            ViewBag.SelectedTeamName = teamName;

            //check if a team has been selected, and return info for that team
            if (teamId != null)
            {
                var bowlers = _repo.Bowlers.Where(x => x.TeamID == teamId).Include(x => x.Team).OrderBy(x => x.BowlerFirstName).ToList();

                return View(bowlers);
            }
            else
            {
                var bowlers = _repo.Bowlers.OrderBy(x => x.BowlerFirstName).Include(x => x.Team).ToList();

                return View(bowlers);
            }
            
            
        }

        [HttpGet]
        public IActionResult Edit(int bowlerId)
        {
            ViewBag.Title = "Edit Bowler Information";
            ViewBag.Teams = _repo.Teams.ToList();
            var b = _repo.Bowlers.FirstOrDefault(x => x.BowlerID == bowlerId);
            return View("Form", b);
        }

        [HttpPost]
        public IActionResult Edit(Bowler b)
        {
            _repo.SaveBowler(b);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Title = "Add New Bowler";
            var b = new Bowler();
            ViewBag.Teams = _repo.Teams.ToList();
            return View("Form", b);
        }

        [HttpPost]
        public IActionResult Add(Bowler b)
        {
            if (ModelState.IsValid)
            {
                _repo.CreateBowler(b);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Add");
            }
            
        }

        public IActionResult Delete(int bowlerId)
        {
            var b = _repo.Bowlers.FirstOrDefault(x => x.BowlerID == bowlerId);
            _repo.DeleteBowler(b);

            return RedirectToAction("Index");
        }

    }
}
