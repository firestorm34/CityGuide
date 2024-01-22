using CityGuide.Shared.DataAccess;
using Microsoft.AspNetCore.SignalR;
using CityGuide.Shared.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace CityGuide.Shared.Hubs
{
   public  class CityGuideHub : Hub
    {
        CityRepository cityRepository;
        public CityGuideHub(CityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public async Task Get(int id)
        {
            City? city = await cityRepository.Get(id);
            await Clients.Caller.SendAsync("ReceiveCity", city);
        }

        public async Task GetAll()
        {
           List<City> cities = await cityRepository.GetAll();
           await Clients.Caller.SendAsync("ReceiveCities", cities);
        }

        public async Task DeleteCity( int id)
        {
          ResultStatus status =  await cityRepository.Delete(id);
          await Clients.Caller.SendAsync("ReceiveDeleteStatus", status); 
          if( status== ResultStatus.Success) {
               await Clients.All.SendAsync("ReceiveDeletedCityId", id);
          }
          
        }

        public async Task UpdateCity(City city)
        {
            ResultStatus status = await cityRepository.Update(city);
            await Clients.Caller.SendAsync("ReceiveUpdateStatus", status);
            if (status == ResultStatus.Success) {
                await Clients.AllExcept(new List<String>{ this.Context.ConnectionId}).
                SendAsync("ReceiveUpdatedCity", city);
            }
           
        }

        public async Task CreateCity( City city)
        {
            ResultStatus status = await cityRepository.Create(city);
            List<City> cities = await cityRepository.GetAll();
            await Clients.AllExcept(new List<string> { this.Context.ConnectionId }).
                SendAsync("ReceiveCities", cities);
            await Clients.Caller.SendAsync("ReceiveCreateStatus", status);
        }

    }
}
