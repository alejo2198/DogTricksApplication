using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using DogTricksApplication.Models;
using DogTricksApplication.Models.ViewModels;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace DogTricksApplication.Controllers
{
    public class DogController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DogController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44366/api/");
        }
        // GET: Dog/List
        public ActionResult List()
        {
            //communicate with our Dogdata api to retrieve list of dogs
            //curl https://localhost:44366/api/dogdata/listdogs

            string url = "dogdata/listdogs";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DogDto> dogs = response.Content.ReadAsAsync< IEnumerable<DogDto>>().Result;

            return View(dogs);
        }

        // GET: Dog/Details/5
        public ActionResult Details(int id)
        {
            //communicate with our Dogdata api to retrieve a dog by id, be able to add a trick to a dog
            //curl https://localhost:44366/api/dogdata/finddog/9

            TeachDogATrick ViewModel = new TeachDogATrick();

            string url = "dogdata/finddog/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DogDto selectedDog = response.Content.ReadAsAsync<DogDto>().Result;
            ViewModel.SelectedDog = selectedDog;

            url = "trickdata/listtricks";
             response = client.GetAsync(url).Result;
            IEnumerable<TrickDto> tricks = response.Content.ReadAsAsync<IEnumerable<TrickDto>>().Result;
            ViewModel.TrickOptions = tricks;

            url = "DogxTrickData/ListDogxTricksforDog/" + id; 
            response = client.GetAsync(url).Result;
            IEnumerable<DogxTrickDto> tricksfordog = response.Content.ReadAsAsync<IEnumerable<DogxTrickDto>>().Result;

           List <TrickDto> tricksLearnt = new List<TrickDto>();
            tricksfordog.ToList().ForEach(trickLearnt =>
            { 
            url = "trickdata/ListTricksforDog/" + trickLearnt.DogTrickId;
                response = client.GetAsync(url).Result;
            TrickDto selectedTrick = response.Content.ReadAsAsync<TrickDto>().Result; 
            tricksLearnt.Add(selectedTrick);
        });
            
            ViewModel.TricksLearnt = tricksLearnt;

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult LearnTrick(int DogId, int TrickId, DogxTrick dogxTrick)
        {

            string url = "dogxtrickdata/adddogxtrick";
            Debug.WriteLine(dogxTrick);
            string jsonpayload = jss.Serialize(dogxTrick);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        // GEt: Dog/New
        public ActionResult New()
        {
            return View();
        }

        // Post: Dog/Create
        [HttpPost]
        public ActionResult Create(Dog dog)
        {
           //For the mvp, the owner is hardcoded, I will add the dog image and dog owner in later stages after the lessons.
            //communicate with our Dogdata api to add a dog
            //curl  -d ""  - "Content-type:application" https://localhost:44366/api/dogdata/AddDog

            string url = "dogdata/adddog";
           
            string jsonpayload = jss.Serialize(dog);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
           HttpResponseMessage response =  client.PostAsync(url,content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
     
        }

        //Get:Dog/Update/5
        public ActionResult Update(int id)
        {
            string url = "dogdata/finddog/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DogDto selectedDog = response.Content.ReadAsAsync<DogDto>().Result;

            return View(selectedDog);
        }

        // GET: Dog/Edit/5
        [HttpPost]
        public ActionResult Edit(Dog dog)
        {
            
            string url = "dogdata/updatedog/" + dog.DogId;

            string jsonpayload = jss.Serialize(dog);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
          

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details/" + dog.DogId);
            }
            else
            {
                return RedirectToAction("Error");
            }
          
        }

        // GET: Dog/Delete/5
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            string url = "dogdata/finddog/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DogDto selectedDog = response.Content.ReadAsAsync<DogDto>().Result;

            return View(selectedDog);
         
        }

        // POST: Dog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                // TODO: Add delete logic here
                string url = "dogdata/deletedog/" + id;
                HttpContent content = new StringContent("");
                content.Headers.ContentType.MediaType = "application/json";
                HttpResponseMessage response = client.PostAsync(url, content).Result;

                return RedirectToAction("List");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }


        public ActionResult Error()
        {
            return View();
        }
    }
}
