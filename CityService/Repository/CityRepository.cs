using CityService.Interface;
using CityService.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CityService.Repository
{
    public class CityRepository : ICity

    {
        CityServiceDbContext dbContext;
        public CityRepository(CityServiceDbContext dbContext) { 
            this.dbContext = dbContext;
        }


        public async Task<IEnumerable<City>> GetAllData()
        {
            var res = await dbContext.Cities.ToListAsync();
            return res;
        }
        public async Task<City> GetCityById(int cityId)
        {
            return  await dbContext.Cities.FirstOrDefaultAsync(c => c.CityId == cityId);
            //if (res == null) return null;
            //return res;
        }
        public async Task<bool> AddCity(City city)
        {
            /*  if (city == null) return false;
              var res=await dbContext.Cities.FirstOrDefaultAsync(c=>c.CityId==city.CityId);
              if (res == null) return false;*/
            try
            {
                dbContext.Cities.Add(city);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCity(int cityId)
        {
            var res = await dbContext.Cities.FirstOrDefaultAsync(c => c.CityId == cityId);
            if (res == null) return false;
            dbContext.Cities.Remove(res);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<City> UpdateAirportCharge(int cityId, int airportCharge)
        {
            if(airportCharge== 0) return null;
            var res=await dbContext.Cities.FirstOrDefaultAsync(c=>c.CityId== cityId);
            if(res == null) return null;
            res.AirportCharge = airportCharge;
            await dbContext.SaveChangesAsync();
            return res;
        }

        public async Task<City> UpdateCity(int cityId, City city)
        {
            if(city== null || cityId==0) return null;
            var res = await dbContext.Cities.FirstOrDefaultAsync(c => c.CityId == cityId);
            if (res == null) return null;
            res.State = city.State;
            res.CityCode = city.CityCode;
            res.CityName = city.CityName;
            await dbContext.SaveChangesAsync();
            return res;
        }
    }
}
