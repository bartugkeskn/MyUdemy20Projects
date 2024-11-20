﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project6_ApiWeather.Context;
using Project6_ApiWeather.Entities;

namespace Project6_ApiWeather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeathersController : ControllerBase
    {
        WeatherContext context = new WeatherContext();

        [HttpGet]  // API projelerinde her metodun mutlaka bir attribute' u olmalı 
        public IActionResult WeatherCityList()
        {
            var values = context.Cities.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateWeatherCity(City city)
        {
            context.Cities.Add(city);
            context.SaveChanges();
            return Ok("Şehir eklendi");
        }

        [HttpDelete]
        public IActionResult DeleteWeatherCity(int id)
        {
            var value = context.Cities.Find(id);
            context.Cities.Remove(value);
            context.SaveChanges();
            return Ok("Şehir Silindi");
        }

        [HttpPut]
        public IActionResult UpdateWeatherCity(City city)
        {
            var value = context.Cities.Find(city.CityId);
            value.CityName = city.CityName;
            value.Detail = city.Detail;
            value.Temp = city.Temp;
            value.Country = city.Country;
            context.SaveChanges();
            return Ok("Şehir Güncellendi");
        }

        [HttpGet("GetWeatherCityById")]  // httpget attribute'unu birden fazla kullanmak istersek yandaki gibi parantez içine methodun ismini yazmalıyız.
        public IActionResult GetWeatherCityById(int id)
        {
            var value = context.Cities.Find(id);
            return Ok(value);
        }

        [HttpGet("TotalCityCount")]
        public IActionResult TotalCityCount()
        {
            var value = context.Cities.Count();
            return Ok(value);
        }

        [HttpGet("MaxTempCityName")]
        public IActionResult MaxTempCity()
        {
            var value = context.Cities.OrderByDescending(x=>x.Temp).Select(y=> y.CityName).FirstOrDefault();
            return Ok(value);
        }
    }
}