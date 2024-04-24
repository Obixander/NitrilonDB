let EventContainer = document.querySelector('.EventContainer');
let second = document.querySelector('.second')

const GetEventsURL = 'https://localhost:7239/api/Event';
GetEvents()

let RatingContainer = document.querySelectorAll('.RatingContainer')
let h2 = document.querySelectorAll('h2')
console.log(RatingContainer)
let GreenBar = document.querySelector('#GreenBar')
let YellowBar = document.querySelector('#YellowBar')
let RedBar = document.querySelector('#RedBar')


function GetEvents() {

   const requestOptions =
   {
      method: 'get',
      headers: {
         'Content-Type': 'application/json'
      },
   }

   fetch(GetEventsURL, requestOptions)
      .then(response => {
         if (!response.ok) {
            throw new Error('Network response was not ok');
         }
         return response.text();
      })
      .then(data => {
         let Events = JSON.parse(data);
         
         
         //try to add a scrollbar to slide through the events
         Events.forEach(Event => {
            if (Event != " ") {
               let EventCard = document.createElement('div');
               let text = document.createElement('p')
               EventCard.classList.add("EventCard")
               const formattedDate = new Date(Event.date).toLocaleDateString();
               text.textContent = Event.name + " - (" + formattedDate + ")";
               second.appendChild(EventCard);
               EventCard.appendChild(text);

               //works
               EventCard.addEventListener('click', function (OnClick) {
                  OnClick.preventDefault();
                  let EventId = Event.id;
                  console.log(EventId)                  
                  GetRatingsByEvent(Event.id)
               });
            }
         });
         let EventCard = document.querySelectorAll('.EventCard')


      })
      .catch(error => {
         console.error('Error:', error);
      });
}

function GetRatingsByEvent(Id) {
   let GetRatingByIdURL = `https://localhost:7239/api/EventRating?eventId=${Id}`;   

   const RequestOptions = {
      method: 'get',
      headers: {
         'Content-Type': 'application/json'
      },
   }

   fetch(GetRatingByIdURL, RequestOptions)
      .then(response => {
         if (!response.ok) {
            throw new Error('Network response was not ok');
         }
         return response.text();
      })
      .then(data => {
         let Ratings = JSON.parse(data);
         console.log(Ratings)
         let Happy = Ratings.goodRatingCount;
         let Neutral = Ratings.neutralRatingCount;
         let Sad = Ratings.badRatingCount;   
         let Total = Happy + Neutral + Sad;      

         console.log(Happy + " " + Neutral + " " + Sad)
         
         if (Happy != 0 || Neutral != 0 || Sad != 0) {
            GreenBar.style.height = `${0}%`;
            YellowBar.style.height = `${0}%`;
            RedBar.style.height = `${0}%`;

            //Procent Calc
            let HappyProcent = Math.round((Happy / Total) * 100);
            h2[0].textContent = `Procent: ${HappyProcent}%`;

            let NeutralProcent = Math.round((Neutral / Total) * 100);
            h2[2].textContent = `Procent: ${NeutralProcent}%`;

            let SadProcent = Math.round((Sad / Total) * 100);
            h2[4].textContent = `Procent: ${SadProcent}%`;

            //Bar Height

            GreenBar.style.height = `${HappyProcent}%`;
            YellowBar.style.height = `${NeutralProcent}%`;
            RedBar.style.height = `${SadProcent}%`;

            //Total Amount
            h2[1].textContent = `Total Ratings: ${Happy}`;
            h2[3].textContent = `Total Ratings: ${Neutral}`
            h2[5].textContent = `Total Ratings: ${Sad}`
         }
         else {
            //resets if the Rating count is 0
            h2[0].textContent = `Procent: 0%`;
            h2[2].textContent = `Procent: 0%`;
            h2[4].textContent = `Procent: 0%`;

            h2[1].textContent = `Total Ratings: ${0}`;
            h2[3].textContent = `Total Ratings: ${0}`
            h2[5].textContent = `Total Ratings: ${0}`

            GreenBar.style.height = `${0}%`;
            YellowBar.style.height = `${0}%`;
            RedBar.style.height = `${0}%`;

         }
      })
      .catch(error => {
         console.error('Error:', error);
      });
}