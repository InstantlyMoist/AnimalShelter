﻿using Client.Models;
using Flurl.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Shared.Enums;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AnimalsController : Controller
    {

        public async Task<IActionResult> Index(AnimalType? animalType, Gender? gender, DeepBoolean? canBeWithKids)
        {
            AnimalIndexModel model = new AnimalIndexModel();
            string baseUrl = "https://localhost:44353/api/animals?";
            if (animalType != null)
            {
                baseUrl += "animalType=" + animalType.ToString() + "&";
                model.animalType = animalType.ToString();
            }
            if (gender != null)
            {
                baseUrl += "gender=" + gender.ToString() + "&";
                model.gender = gender.ToString();
            }
            if (canBeWithKids != null)
            {
                baseUrl += "canBeWithKids=" + canBeWithKids.ToString();
                model.canBeWithKids = canBeWithKids.ToString();
            }
            var response = await baseUrl.GetStringAsync();
            model.animalList = JsonConvert.DeserializeObject<List<Animal>>(response);
            model.animalTypes = new SelectList(Enum.GetValues(typeof(AnimalType)).Cast<AnimalType>().ToList(), animalType.ToString());
            model.genders = new SelectList(Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList(), gender.ToString());
            model.canBeWithKidsList = new SelectList(Enum.GetValues(typeof(DeepBoolean)).Cast<DeepBoolean>().ToList(), canBeWithKids.ToString());
            return View(model);
        }

        [Authorize]
        [HttpGet("showinterest/{id}")]
        public async Task<IActionResult> ShowInterest(int id)
        {
            
            // /animals/showinterest/5
            Interest interest = new Interest();
            interest.Email = User.Identity.Name;

            string response = await ("https://localhost:44353/api/animals/showinterest/" + id).PostJsonAsync(interest).ReceiveString();
            if (response == null) return NotFound();
            ResponseModel model = JsonConvert.DeserializeObject<ResponseModel>(response);
            if (model.Message.Equals("MAX_COUNT")) return View("InterestFailed");
            else if (model.Message.Equals("ALREADY_INTERESTED")) return View("AlreadyInterested");
            return View("InterestShown");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Id = id;
            string response = await ("https://localhost:44353/api/animals/" + id).GetStringAsync();
            if (response == null || response == String.Empty)
            {
                return NotFound();
            }
            Animal animal = JsonConvert.DeserializeObject<Animal>(response);
            return View(animal);
        }
        [Authorize]

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Animal Animal, IFormFile File)
        {

            using (var memoryStream = new MemoryStream())
            {
                
                await File.CopyToAsync(memoryStream);
                Animal.Image = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
            }
            var response = await "https://localhost:44353/api/animals".PostJsonAsync(Animal).ReceiveString();
            ResponseModel Model = JsonConvert.DeserializeObject<ResponseModel>(response);
            return RedirectToAction("Details", Model);
        }

        
    }
}

