// let EventContainer = document.querySelector('.EventContainer');

let Btn = document.querySelector("#Btn");
let counter = 0;
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

const PostMember = 'https://localhost:7239/api/Member';
const GetEventsURL = 'https://localhost:7239/api/Member';
const GetAllMemberships = 'https://localhost:7239/api/Membership';

let MembershipArray = [];
GetEvents()
GetMemberships()
function GetMemberships() {
   const requestOptions =
   {
      method: 'get',
      headers: {
         'Content-Type': 'application/json'
      },
   }
   
   fetch(GetAllMemberships, requestOptions)
      .then(response => {
         if (!response.ok) {
            throw new Error('Network response was not ok');
         }
         return response.text();
      })
      .then(data => {
         let Memberships = JSON.parse(data);
         MembershipArray = Memberships;
         console.log(Memberships);
      })
      .catch(error => {
         console.error('Error:', error);
      });
   }
   
   Btn.addEventListener('click', function(OnClick) {
      AddMember();
});

function AddMember() {
   console.log("BTN CLICKED")
   let FormContainer = document.querySelector(".AddMember");
   FormContainer.classList.toggle("AddMember");
   FormContainer.classList.toggle("AddMemberActive");
   
   let MembershipInput = document.querySelector("#SelectMembership");
   MembershipArray.forEach(Membership => {
      let Option = document.createElement("option");
      Option.value = Membership.membershipId;
      Option.textContent = Membership.name;
      MembershipInput.appendChild(Option);
   });
   
   let Submit = document.querySelector("#Submit");
   Submit.addEventListener('click', function(OnClick) {
      OnClick.preventDefault()
      SaveMember();
   });
   
}

function SaveMember() {
   let Name = document.querySelector("#Name");
   let Email = document.querySelector("#Email");
   let PhoneNumber = document.querySelector("#PhoneNumber");
   let Membership = document.querySelector("#SelectMembership");
   console.log(Membership.value)
   
   const requestOptions =
   {
      method: 'post',
      headers: {
         'Content-Type': 'application/json'
      },
      body: JSON.stringify({
         name: Name.value,
         email: Email.value,
         phoneNumber: PhoneNumber.value,
         membership: {
            membershipId: Membership.value,
            name: Membership.value,
            description: Membership.value
         }
      })
   }

   fetch (PostMember, requestOptions)
   .then(response => {
      if (!response.ok) {
         throw new Error('Network response was not ok');
      }
      return response.text();
   })
   .then(data => {
      location.reload()
   })
   .catch(error => {
      console.error('Error:', error);
   });
   
}

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
               let MemberCardOptions = document.createElement('div');
               MemberCardOptions.classList.add("Option")

               MemberCardId.textContent = "Id: " + Member.memberId;
               MemberCardName.textContent = "Name: " + Member.name;
               MemberCardEmail.textContent = "Email: " + Member.email;
               MemberCardPhone.textContent = "Phone: " + Member.phoneNumber;
               MemberCardDate.textContent = "Date: " + (Member.date).substring(0, 10);
               MemberCardMembership.textContent = "Membership: " + Membership.name;
               MemberCardOptions.textContent = "⚙️";

               MemberCardContainer.appendChild(MemberCardId);
               MemberCardContainer.appendChild(MemberCardName);
               MemberCardContainer.appendChild(MemberCardEmail);
               MemberCardContainer.appendChild(MemberCardPhone);
               MemberCardContainer.appendChild(MemberCardDate);
               MemberCardContainer.appendChild(MemberCardMembership);
               MemberCardContainer.appendChild(MemberCardOptions);

               MemberCardOptions.addEventListener('click', function(OnClick) {          
                  if (MemberCardOptions.classList.contains("OptionActive")) {
                     MemberCardOptions.classList.remove("OptionActive");

                     let Options = document.querySelector(".Active")
                     Options.remove();
                  } else {
                     MemberCardOptions.classList.add("OptionActive");
                     let Options = document.createElement("div");
                     Options.classList.add("Active");
                     MemberCardOptions.appendChild(Options);

                     let Edit = document.createElement('p')
                     let Remove = document.createElement('p');
                     Edit.textContent = "EDIT";
                     Remove.textContent = "REMOVE";
                     Options.appendChild(Edit)
                     Options.appendChild(Remove)

                     Edit.addEventListener("click", function(OnClick){
                        console.log("Edit Was Pressed")
                        EditUser(Member);
                     });
                     Remove.addEventListener("click", function(OnClick){
                        console.log("Remove Was Pressed")
                        RemoveUser(Member);
                     });

                  }
               });

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

function EditUser(Member) {
 
}
function RemoveUser(Member) {
   const RemoveUrl = `https://localhost:7239/api/Member/${Member.memberId}`;
   const requestOptions = {
      method: 'delete',
      headers: {
         'Content-Type': 'application/json'
      },      
   }
   fetch (RemoveUrl, requestOptions)
   .then(response => {
      if (!response.ok) {
         throw new Error('Network response was not ok');
      }
      return response.text();
   })
   .then(data => {
      location.reload()
   })
   .catch(error => {
      console.error('Error:', error);
   });
}
