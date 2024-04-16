let btn1 = document.querySelector('#btn1');
let btn2 = document.querySelector('#btn2');
let btn3 = document.querySelector('#btn3');
//this is used for the timer in the overlay
let Cooldown = 0;

//Methods for Posting to Database on button Click
//add A method for this and use the button id for the method
const EventRatingURL = 'https://localhost:7239/api/EventRating';

btn1.addEventListener('click', function (OnClick) {
   OnClick.preventDefault();
   let id = 1;
   AddRating(id);
});
btn2.addEventListener('click', function (OnClick) {
   OnClick.preventDefault();
   let id = 2;
   AddRating(id);
});
btn3.addEventListener('click', function (OnClick) {
   OnClick.preventDefault();
   let id = 3;
   AddRating(id);
});

//this is used to get all Events from the database that has the same date or later
GetEvents()

function GetEvents() {
   let Year = new Date().getFullYear()
   let Month = new Date().getMonth()
   let Day = new Date().getDate() 
   
   console.log(Year)
   
   const GetEventUrl = `https://localhost:7239/api/Event/GetActiveOrFutureEvents?year=${2026}&month=${Month}&day=${Day}&dayOfWeek=0`;

   const requestOptions = 
   {
      method: 'get',
      headers: {
         'Content-Type': 'application/json'
      },      
   }



   fetch(GetEventUrl, requestOptions)
      .then(response => {
         if (!response.ok) {
            throw new Error('Network response was not ok');
         }
         return response.text();
      })
      .then(data => {
         console.log(data)
      })
      .catch(error => {
         console.error('Error:', error);
      });
}





//this is the method that posts the rating to the database
function AddRating(id) {
   const requestOptions = {
      method: 'post',
      headers: {
         'Content-Type': 'application/json'
      },
      body: JSON.stringify({
         //Add A method for to change the EventId
         "EventId": "2",

         "RatingID": id,
      })
   };  

   

   fetch(EventRatingURL, requestOptions)
      .then(response => {
         if (!response.ok) {
            throw new Error('Network response was not ok');
         }
         return response.text();
      })
      .then(data => {

      })
      .catch(error => {
         console.error('Error:', error);
      });
   AddOverlay();
}
//This is used to create the overlay so the user gets some feedback to their rating
function AddOverlay() {
   //Cooldown for the overlay to pervent spam
   const Timer = Date.now();

   if (Timer > Cooldown) {
      let Overlay = document.createElement('div');
      Overlay.classList.toggle('overlay');
      
      let text = document.createElement("h1")
      text.classList.toggle('text');
      text.textContent = "Tak for dit Feedback!";
      Overlay.append(text);

      let Body = document.querySelector("body")
      Body.append(Overlay);

      setTimeout(function () {
         Overlay.remove()
         text.remove()
      }, 5000);

      //Set the cooldown to the current time + 5 seconds
      Cooldown = Date.now();
      Cooldown = Math.floor(Cooldown + 5000);

      //Used for debuging
      console.log(Cooldown)
      console.log(Math.floor(Timer - Cooldown))
   }
   //If the cooldown is not over, do nothing
   else {
      console.log("Wait Please")
   }

}