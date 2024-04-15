let btn1 = document.querySelector('#btn1');
let btn2 = document.querySelector('#btn2');
let btn3 = document.querySelector('#btn3');
let Div = document.querySelector('#Events');



// console.log("button clicked")
// btn.addEventListener('click', function (event) {
//   event.preventDefault();
//    console.log("button clicked")
//   const requestOptions = {
//     method: 'get',
//     headers: {
//       'Content-Type': 'application/json'
//     },
//   };

//   const test = fetch(apiUrl, requestOptions)
//     .then(response => {
//       if (!response.ok) {
//         throw new Error('Network response was not ok');
//       }
//       return response.text();
//     })
//     .then(data => {      
//       Div.textContent = data;
//     })
//     .catch(error => {
//       console.error('Error:', error);
//     });

//     console.log(test);
    
    
// });


//Methods for Posting to Database on button Click
//add A method for this and use the button id for the method
const EventRatingURL = 'https://localhost:7239/api/EventRating';
btn1.addEventListener('click', function (event) {
   event.preventDefault();
   let id = 1;
   AddRating(id);
});
btn2.addEventListener('click', function (event) {
   event.preventDefault();
   let id = 2;
   AddRating(id);
});
btn3.addEventListener('click', function (event) {
   event.preventDefault();
   let id = 3;
   AddRating(id);
});

function AddRating(id) {   
   const requestOptions = {
      method: 'post',
      headers: {
         'Content-Type': 'application/json'
      },
      body: JSON.stringify({
         //Change this EventId later
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
}