using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CityGuide.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CityGuide.Shared.DataAccess
{
    public class CityRepository
    {
        private readonly CityGuideContext context;
        public CityRepository(CityGuideContext guideContext)
        {
            context = guideContext;
        }

        public async Task<List<City>> GetAll()
        {
            
            return await context.Cities.AsNoTracking().ToListAsync();
        }
       
        public async Task<City?> Get (int id)
        {

            City? returnCity =  await context.Cities.AsNoTracking().FirstOrDefaultAsync(city => city.Id == id);
            return returnCity;
        }

        public async Task<ResultStatus> Delete(int id)
        {
            City? taskResult = await context.Cities.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (taskResult is City city)
            {
                try
                {
                    context.Cities.Remove(city);
                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return  ResultStatus.Failed;
                }
                
                return  ResultStatus.Success;
            }
            else
            {
                return  ResultStatus.NotFound;
            }

        }

        public async Task<ResultStatus> Update(City city_update)
        {
            if (city_update != null)
            {   
                if(context.Cities.FirstOrDefault(c=>c.Id== city_update.Id) is City city )
                {
                    try {
                        
                        city.Population = city_update.Population;
                        city.FoundationDate = city_update.FoundationDate;
                        city.Name = city_update.Name;
                        await context.SaveChangesAsync();
                        return  ResultStatus.Success;
                    }
                    catch(Exception e)
                    {
                        return  ResultStatus.Failed;
                    }
                }
                else
                {
                    return ResultStatus.NotFound;
                }
            }
            
            else
            {
                return ResultStatus.NullModel;
            }
        }

        public async Task<ResultStatus> Create(City city)
        {
            if(context.Cities.Any(c=>c.Name == city.Name))
            {
                return ResultStatus.AlreadyExists;
            }
            else
            {
                try
                {
                    await context.Cities.AddAsync(city);
                    await context.SaveChangesAsync();
                    return ResultStatus.Success;
                }
                catch(Exception e)
                {
                    return ResultStatus.Failed;
                }
            }
        }
    }
}
