// let EventContainer = document.querySelector('.EventContainer');
let second = document.querySelector('.second')



//New Attempt

let SearchBar = document.querySelector("#search");
let FilterSearch = document.querySelector("#SearchFilter");
SearchBar.placeholder = "Search by " + FilterSearch.value; 
FilterSearch.addEventListener('change', function (OnChange) {
SearchBar.placeholder = "Search by " + FilterSearch.value; 
}); 

let MemberContainer = document.querySelector('.MemberContainer');

const GetEventsURL = 'https://localhost:7239/api/Member';
GetEvents()

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
         let Members = JSON.parse(data);                
         console.log(Members)

         Members.forEach(Member => {
            let Membership = Member.membership;
            if (Member != " " && Membership.membershipId != 4) {
               let MemberCard = document.createElement('div');
               let text = document.createElement('p')

               MemberCard.classList.add("MemberCard")
               
               text.textContent = Member.name + " - (" + Membership.name + ")";
               
               second.appendChild(MemberCard);
               MemberCard.appendChild(text);

               //works
               MemberCard.addEventListener('click', function (OnClick) {
                  OnClick.preventDefault();
                  DisplayMemberInfo(Member)
               });
            }
         });
         //??
         let MemberCard = document.querySelectorAll('.EventCard')


      })
      .catch(error => {
         console.error('Error:', error);
      });
}

function DisplayMemberInfo(Member) {
   console.log(Member)

}

