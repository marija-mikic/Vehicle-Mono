using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System;
using Vehicle_Mono.ViewModel;
using Vehicle_Mono_DAL.Interface;
using Vehicle_Mono_DAL;
using Vehicle_Mono_BL.Interface;
using Vehicle_Mono_BL.Paged;
using Vehicle_Mono_BL.Selector;

namespace Vehicle_Mono.Controlers
{
    public class MakesController : Controller
    {
         
            private readonly MonoVehicleContext _context;
            private readonly IMakeVehicle _service;
            private readonly IMapper _mapper;
            private readonly IMake makes;


            public MakesController(MonoVehicleContext context,
                IMakeVehicle service,
                IMapper mapper,
                IMake make
                 )
            {
                this._context = context;
                this._service = service;
                this._mapper = mapper;
                this.makes = make;

            }


            public async Task<IActionResult> Index(
                string searchTerm,
                string currentFilter,
                string sortBy = "Name",
                string sortOrder = "asc",
                int pageNumber = 1,
                 int pageSize = 5)
            {
                if (searchTerm != null)
                {
                    pageNumber = 1;

                }
                else
                {
                    searchTerm = currentFilter;
                }

                ViewData["CurrentFilter"] = searchTerm;
                ViewData["SortBy"] = sortBy;
                ViewData["SortOrder"] = sortOrder;

                var sorting = new Sorting(sortBy, sortOrder == "asc");
                var make = await _service.GetAll(new Paging(pageNumber, pageSize), sorting, searchTerm);
                //https://docs.automapper.org/en/stable/Custom-type-converters.html

                var mapingList = _mapper.Map<PaginatedList<MakeViewModel>>(make);
                return View(mapingList);
            }

            //GET: Makes1/Details/5
            public async Task<IActionResult> Details(int id)
            {
                MakeViewModel make;
                try
                {
                    make = _mapper.Map<MakeViewModel>(await _service.GetMakeById(id));

                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                return View(make);
            }

            //GET: Makes/Create
            public IActionResult Create()
            {
                return View();
            }

            //// POST: Makes1/Create
            //// To protect from overposting attacks, enable the specific properties you want to bind to.
            //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] MakeViewModel makes)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await _service.Add(_mapper.Map<IMake>(makes));
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception exc)
                    {

                        ModelState.AddModelError(string.Empty, exc.Message);
                        return View(makes);
                    }
                }
                return View(makes);
            }

            // GET: Makes1/Edit/5
            public async Task<IActionResult> Edit(int id)
            {
                MakeViewModel make;
                try
                {
                    make = _mapper.Map<MakeViewModel>(await _service.GetMakeById(id));
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                return View(make);
            }

            //    // POST: Makes1/Edit/5
            //    // To protect from overposting attacks, enable the specific properties you want to bind to.
            //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("MakeId,Name,Abrv")] MakeViewModel makes)
            {
                if (id != makes.MakeId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        bool edited = await _service.Edit(_mapper.Map<IMake>(makes));
                        if (!edited)
                        {
                            return NotFound();
                        }
                    }
                    catch (Exception)
                    {
                        ViewData["error"] = "Error";
                        return View(makes);
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(makes);
            }

            // GET: Makes1/Delete/5
            public async Task<IActionResult> Delete(int id)
            {
                MakeViewModel make;
                try
                {
                    make = _mapper.Map<MakeViewModel>(await _service.GetMakeById(id));
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                return View(make);

            }

            // POST: Makes1/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                try
                {
                    bool confirm = await _service.Remove(id);
                    if (!confirm)
                    {
                        return NotFound();
                    }

                }
                catch (KeyNotFoundException)
                {
                    return (StatusCode(500));
                }
                return RedirectToAction(nameof(Index));
            }

            //    private bool MakesExists(int id)
            //    {
            //      return (_context.Makes?.Any(e => e.Id == id)).GetValueOrDefault();
            //    }
        }
    }
 
