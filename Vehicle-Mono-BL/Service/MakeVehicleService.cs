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
using Vehicle_Mono_DAL.MODELS;
using Vehicle_Mono_DAL;
using Microsoft.EntityFrameworkCore;
using Vehicle_Mono_BL.Selector;
using Vehicle_Mono_BL.Paged;

namespace Vehicle_Mono_BL.Service
{
    public class MakeVehicleService : IMakeVehicle
    {

        private readonly MonoVehicleContext _contex;
        private readonly IMapper _mapper;



        public MakeVehicleService(MonoVehicleContext contex, IMapper mapper)
        {

            _contex = contex;
            _mapper = mapper;

        }


        public async Task<bool> Add(IMake make)
        {
            try
            {
                _contex.Makess.Add(_mapper.Map<MakeEntity>(make));
                return await _contex.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Edit(IMake make)
        {
            try
            {
                var edit = await _contex.Makess.FindAsync(make.MakeId);
                if (edit == null)
                {
                    return false;

                }
                _mapper.Map(make, edit);
                await _contex.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IMake> GetMakeById(int id)
        {
            var make = await _contex.Makess.FindAsync(id);
            if (make == null)
            {
                throw new KeyNotFoundException("make id not valid");
            }
            else return _mapper.Map<Makes>(make);

        }

        public async Task<List<IMake>> GetAll()
        {
            try
            {
                var data = from m in _contex.Makess select m;

                data = MakeSort(data, new Sorting("Name", true));

                var items = await data.ToListAsync();
                return new List<IMake>(_mapper.Map<List<Makes>>(items));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        //public async Task<PaginatedList<IMake>> GetAll(Paging paging, Sorting sorting, string searchTerm)
        //{
        //    try
        //    {
        //        var make = from m in _contex.Makess select m;
        //        if (!string.IsNullOrEmpty(searchTerm))
        //        {
        //            make = make.Where(m => m.Name.Contains(searchTerm)) ; 
        //        }
        //        make = MakeSort(make, sorting);

        //        var count = await make.CountAsync();

        //        var pageSize = await make.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToListAsync();

        //        //int pageSize= paging.PageSize;
        //        var mapingItem = new List<IMake>(_mapper.Map<List<Makes>>(pageSize));

        //        return new PaginatedList<IMake>(mapingItem, count, paging.PageSize, paging.PageNumber);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<PaginatedList<IMake>> GetAll(Paging paginationSettings, Sorting sortSettings, string searchTerm)
        {
            try
            {
                var data = from m in _contex.Makess select m;

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    data = data.Where(m => m.Name.ToUpper().Contains(searchTerm.ToUpper()) || m.Abrv.ToUpper().Contains(searchTerm.ToUpper()));
                }

                data = MakeSort(data, sortSettings);

                var count = await data.CountAsync();
                var items = await data.Skip((paginationSettings.PageNumber - 1) * paginationSettings.PageSize).Take(paginationSettings.PageSize).ToListAsync();
                var interfaceTypeItems = new List<IMake>(_mapper.Map<List<Makes>>(items));

                return new PaginatedList<IMake>(interfaceTypeItems, count, paginationSettings.PageNumber, paginationSettings.PageSize);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public IQueryable<MakeEntity> MakeSort(IQueryable<MakeEntity> make, Sorting sorting)
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

        public async Task<bool> Remove(int id)
        {
            try
            {
                var remove = await _contex.Makess.FindAsync(id);
                if (remove == null)
                {
                    return false;
                }
                _contex.Makess.Remove(remove);

                return await _contex.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {



                throw new Exception(ex.Message);

            }
        }

    }
}
