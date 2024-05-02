// let EventContainer = document.querySelector('.EventContainer');
let second = document.querySelector('.second')



//New Attempt
let MembersArray = [];

let SearchBar = document.querySelector("#search");
let FilterSearch = document.querySelector("#SearchFilter");
SearchBar.placeholder = "Search by " + FilterSearch.value; 

FilterSearch.addEventListener('change', function (OnChange) {
SearchBar.placeholder = "Search by " + FilterSearch.value;
}); 
SearchBar.addEventListener('input', function (OnInput) {
   console.log(SearchBar.value);
   Search(SearchBar.value);
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
         MembersArray = Members;
         Members.forEach(Member => {
            let Membership = Member.membership;
            if (Member != " " && Membership.membershipId != 4) {
               //Change this so that Membercard is a container that has the member info as cards inside of it
               let MemberCardContainer = document.createElement('div');
               MemberCardContainer.classList.add("MemberCardContainer")
               MemberContainer.appendChild(MemberCardContainer);

               let MemberCardId = document.createElement('div');
               let MemberCardName = document.createElement('div');
               let MemberCardEmail = document.createElement('div');
               let MemberCardPhone = document.createElement('div');
               let MemberCardDate = document.createElement('div');
               let MemberCardMembership = document.createElement('div');

               MemberCardId.textContent = "Id: " + Member.memberId;
               MemberCardName.textContent = "Name: " + Member.name;
               MemberCardEmail.textContent = "Email: " + Member.email;
               MemberCardPhone.textContent = "Phone: " + Member.phoneNumber;
               MemberCardDate.textContent = "Date: " + (Member.date).substring(0, 10);
               MemberCardMembership.textContent = "Membership: " + Membership.name;

               MemberCardContainer.appendChild(MemberCardId);
               MemberCardContainer.appendChild(MemberCardName);
               MemberCardContainer.appendChild(MemberCardEmail);
               MemberCardContainer.appendChild(MemberCardPhone);
               MemberCardContainer.appendChild(MemberCardDate);
               MemberCardContainer.appendChild(MemberCardMembership);

               // Work on later
               // MemberCardContainer.addEventListener('click', function (OnClick) {
               //    OnClick.preventDefault();                  
               // });

            }
         });


      })
      .catch(error => {
         console.error('Error:', error);
      });
}

function Search(SearchQuery) {
   let FilteredArray = MembersArray.filter(Member => {
      let Membership = Member.membership;

      return Member.name.toLowerCase().includes(SearchQuery.toLowerCase()) ||      
         Member.email.toLowerCase().includes(SearchQuery.toLowerCase()) ||
         Member.phoneNumber.toLowerCase().includes(SearchQuery.toLowerCase()) ||
         Membership.name.toLowerCase().includes(SearchQuery.toLowerCase()) ||
         (Member.date).substring(0, 10).toLowerCase().includes(SearchQuery.toLowerCase());
   });

   MemberContainer.innerHTML = "";
   FilteredArray.forEach(Member => {
      let Membership = Member.membership;
      if (Member != " ") {
         let MemberCardContainer = document.createElement('div');
         MemberCardContainer.classList.add("MemberCardContainer")
         MemberContainer.appendChild(MemberCardContainer);

         let MemberCardId = document.createElement('div');
         let MemberCardName = document.createElement('div');
         let MemberCardEmail = document.createElement('div');
         let MemberCardPhone = document.createElement('div');
         let MemberCardDate = document.createElement('div');
         let MemberCardMembership = document.createElement('div');

         MemberCardId.textContent = "Id: " + Member.memberId;
         MemberCardName.textContent = "Name: " + Member.name;
         MemberCardEmail.textContent = "Email: " + Member.email;
         MemberCardPhone.textContent = "Phone: " + Member.phoneNumber;
         MemberCardDate.textContent = "Date: " + (Member.date).substring(0, 10);
         MemberCardMembership.textContent = "Membership: " + Membership.name;

         MemberCardContainer.appendChild(MemberCardId);
         MemberCardContainer.appendChild(MemberCardName);
         MemberCardContainer.appendChild(MemberCardEmail);
         MemberCardContainer.appendChild(MemberCardPhone);
         MemberCardContainer.appendChild(MemberCardDate);
         MemberCardContainer.appendChild(MemberCardMembership);
      }
   });
}


