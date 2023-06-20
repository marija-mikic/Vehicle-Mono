using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Vehicle_Mono_BL.Interface;
using Vehicle_Mono_DAL.Entity;
using Vehicle_Mono_DAL.Interface;
using Vehicle_Mono_DAL;
using Microsoft.EntityFrameworkCore;
using Vehicle_Mono_DAL.Models;
using Vehicle_Mono_BL.Paged;
using Vehicle_Mono_BL.Selector;

namespace Vehicle_Mono_BL.Service
{
    public class ModelVehicleService : IModelVehicle
    {
        private readonly MonoVehicleContext _context;
        private readonly IMapper _mapper;

        public ModelVehicleService(MonoVehicleContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Add(IModelV model)
        {
            try
            {
                _context.Models.Add(_mapper.Map<ModelEntity>(model));
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                // throw new Exception(ex.Message);
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<PaginatedList<IModelV>> GetAllModels(Paging paging, Sorting sorting, Filtering filtering)
        {
            try
            {
                var data = from m in _context.Models select m;


                data = FilterbyMakes(data, filtering);
                data = MakeSort(data, sorting);

                var count = await data.CountAsync();
                //var items = await data.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();
                var items = await data.Include(m => m.Make).Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();

                var interfaceTypeItems = new List<IModelV>(_mapper.Map<List<Model>>(items));

                return new PaginatedList<IModelV>(interfaceTypeItems, count, paging.PageNumber, paging.PageSize);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IModelV> GetModelById(int id)
        {
            var model = await _context.Models.Include(m => m.Make).FirstOrDefaultAsync(m => m.ModelId == id);
            // include= ubacivanje marke u ispis html
            if (model == null)
            {
                throw new KeyNotFoundException("Model with id not found");
            }
            else return _mapper.Map<Model>(model);

        }

        public async Task<bool> Edit(IModelV model)
        {
            try
            {
                var toUpdate = await _context.Models.FindAsync(model.ModelId);
                if (toUpdate == null)
                {
                    return false;
                }
                _mapper.Map(model, toUpdate);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var deleted = await _context.Models.FindAsync(id);
                if (deleted == null)
                {
                    return false;
                }
                _context.Models.Remove(deleted);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static IQueryable<ModelEntity> FilterbyMakes(IQueryable<ModelEntity> data, Filtering filtering)
        {
            if (!string.IsNullOrEmpty(filtering.Name))
            {
                data = data.Where(m => m.Name.ToUpper().Contains(filtering.Name.ToUpper()));
            }
            if (filtering.MakeId != null & filtering.MakeId > 0)
            {
                data = data.Where(m => m.MakeId == filtering.MakeId);
            }
            return data;
        }

        public IQueryable<ModelEntity> MakeSort(IQueryable<ModelEntity> make, Sorting sorting)
        {
            switch (sorting.SortBy)
            {
                case "Name":
                    if (sorting.SortAscending)
                    {
                        return make.OrderBy(m => m.Name);
                    }
                    else
                    {
                        return make.OrderByDescending(m => m.Name);
                    }
                case "Abrv":
                    if (sorting.SortAscending)
                    {
                        return make.OrderBy(m => m.Abrv);
                    }
                    else
                    {
                        return make.OrderByDescending(m => m.Abrv);
                    }
                default:
                    return make;
            }
        }
    }
}



