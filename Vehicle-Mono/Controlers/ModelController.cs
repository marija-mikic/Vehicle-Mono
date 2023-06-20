using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Vehicle_Mono.ViewModel;
using Vehicle_Mono_BL.Interface;
using Vehicle_Mono_DAL.Interface;
using Vehicle_Mono_DAL;
using Microsoft.EntityFrameworkCore;
using Vehicle_Mono_BL.Selector;
using Vehicle_Mono_BL.Paged;

namespace Vehicle_Mono.Controlers
{
    public class ModelController : Controller
    {
        private readonly MonoVehicleContext _context;
        private readonly IMapper _mapper;
        private readonly IMakeVehicle _vehicleService;
        private readonly IModelVehicle _modelVehicleService;


        public ModelController(
            MonoVehicleContext context,
            IMapper mapper,
            IMakeVehicle vehicleService,
            IModelVehicle modelVehicleService
             )
        {
            this._context = context;
            this._mapper = mapper;
            this._vehicleService = vehicleService;
            this._modelVehicleService = modelVehicleService;
        }


        // GET: Model
        public async Task<IActionResult> Index(
            string searchTerm,
            string filtering,
            int selectedIdMake,
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
                searchTerm = filtering;
            }



            ViewData["Filtering"] = searchTerm;
            ViewData["SortBy"] = sortBy;
            ViewData["SortOrder"] = sortOrder;

            var sorting = new Sorting(sortBy, sortOrder == "asc");
            var model = await _modelVehicleService.GetAllModels(new Paging(pageNumber, pageSize), sorting, new Filtering(selectedIdMake, searchTerm));
            var make = await _vehicleService.GetAll();

            //https://docs.automapper.org/en/stable/Custom-type-converters.html

            // var mapingList = _mapper.Map<PaginatedList<ModelViewModel>>(model, make);
            return View(new AllModelViewModel(model, make));
        }

        // GET: Model/Details/5
        public async Task<IActionResult> Details(int id)
        {
            IModelV model;
            try
            {
                model = await _modelVehicleService.GetModelById(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<ModelViewModel>(model);
            viewModel.Makes = await _vehicleService.GetAll();
            return View(viewModel);
        }

        //    // GET: Model/Create
        public async Task<IActionResult> Create()
        {
            await PrepareDropdownList();
            return View();
        }

        private async Task PrepareDropdownList()
        {
            var makes = await _context.Makess.ToListAsync();

            ViewBag.Makes = new SelectList(makes, "MakeId", "Name");
        }



        //    // POST: Model/Create
        //    // To protect from overposting attacks, enable the specific properties you want to bind to.
        //    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelId,MakeId,Name,Abrv")] ModelViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _modelVehicleService.Add(_mapper.Map<IModelV>(viewModel));
                    //   await _context.SaveChangesAsync();

                    //TempData[Constants.Message] = $"Mjesto {mjesto.NazMjesta} dodano. Id mjesta = {mjesto.IdMjesta}";
                    // TempData[Constants.ErrorOccurred] = false;
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception exc)
                {
                    ModelState.AddModelError(string.Empty, exc.Message);
                    await PrepareDropdownList();
                    return View(viewModel);
                }
            }
            else
            {
                await PrepareDropdownList();
                return View(viewModel);
            }
        }

        //    // GET: Model/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            IModelV model;
            try
            {
                model = await _modelVehicleService.GetModelById(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<ModelViewModel>(model);
            viewModel.Makes = await _vehicleService.GetAll();
            return View(viewModel);
        }

        // POST: Models/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModelId, Name, Abrv, MakeId")] ModelViewModel model)
        {
            if (id != model.ModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bool hasSucceeded = await _modelVehicleService.Edit(_mapper.Map<IModelV>(model));
                    if (!hasSucceeded)
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {
                    ViewData["Error"] = "Error";
                    model.Makes = await _vehicleService.GetAll();
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            model.Makes = await _vehicleService.GetAll();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            IModelV model;
            try
            {
                model = await _modelVehicleService.GetModelById(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            var viewModel = _mapper.Map<ModelViewModel>(model);
            viewModel.Makes = await _vehicleService.GetAll();
            return View(viewModel);
        }

        // POST: Makes1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                bool confirm = await _modelVehicleService.Delete(id);
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
        //private bool ModelEntityExists(int id)
        //{
        //    return (_context.Models?.Any(e => e.ModelId == id)).GetValueOrDefault();
        //}
    }
}

