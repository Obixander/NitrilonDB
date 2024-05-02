// let EventContainer = document.querySelector('.EventContainer');

let Btn = document.querySelector("#Btn");
let state = 1;
let Id = -1;
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


let AddMemberActive = document.querySelector(".AddMember")

let Exit = document.querySelector(".Exit");
Exit.addEventListener("click", function (OnClick) {
   OnClick.preventDefault()
   AddMemberActive.classList.toggle("AddMember")
   AddMemberActive.classList.toggle("AddMemberActive")
});


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

Btn.addEventListener('click', function (OnClick) {
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
   state = 1;
   Submit.addEventListener('click', function (OnClick) {
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

   let requestOptions = {};
   let Url = "";

   console.log(state);
   if (state == 1) {
      Url = PostMember;
      requestOptions = {
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
   }
   else if (state == 2) {
      Url = `https://localhost:7239/api/Member/${Id}`;
      requestOptions = {
         method: 'put',
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
   }
   console.log(Url, requestOptions)
   fetch(Url, requestOptions)
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

               MemberCardOptions.addEventListener('click', function (OnClick) {
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

                     Edit.addEventListener("click", function (OnClick) {
                        console.log("Edit Was Pressed")
                        EditUser(Member);
                     });
                     Remove.addEventListener("click", function (OnClick) {
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
      let FilterMatch = FilterSearch.value
      switch (FilterMatch) {
         case "Name":
            return Member.name.toLowerCase().includes(SearchQuery.toLowerCase());
            break;
         case "Email":
            return Member.email.toLowerCase().includes(SearchQuery.toLowerCase());
            break;
         case "Phone":
            return Member.phoneNumber.toLowerCase().includes(SearchQuery.toLowerCase())
            break;
         case "Date":
            (Member.date).substring(0, 10).toLowerCase().includes(SearchQuery.toLowerCase());
            break;
         default:
            console.log("Error has happend seek help")
            break;
               }
      
      
      
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

         MemberCardOptions.addEventListener('click', function (OnClick) {
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

               Edit.addEventListener("click", function (OnClick) {
                  console.log("Edit Was Pressed")
                  EditUser(Member);
               });
               Remove.addEventListener("click", function (OnClick) {
                  console.log("Remove Was Pressed")
                  RemoveUser(Member);
               });
            }
         });


      }
   });
}

function EditUser(Member) {
   //testing begins
   let FormContainer = document.querySelector(".AddMember");
   FormContainer.classList.toggle("AddMember");
   FormContainer.classList.toggle("AddMemberActive");

   let Name = document.querySelector("#Name");
   Name.value = Member.name;

   let Email = document.querySelector("#Email");
   Email.value = Member.email;

   let PhoneNumber = document.querySelector("#PhoneNumber");
   PhoneNumber.value = Member.phoneNumber;

   let SelectMembership = document.querySelector("#SelectMembership");
   console.dir(SelectMembership);
   SelectMembership.innerHTML = Member.membership.name


   let MembershipInput = document.querySelector("#SelectMembership");
   MembershipArray.forEach(Membership => {
      let Option = document.createElement("option");
      Option.value = Membership.membershipId;
      Option.textContent = Membership.name;
      MembershipInput.appendChild(Option);
   });

   let Submit = document.querySelector("#Submit");
   Submit.innerHTML = "Change Member";

   state = 2;
   Id = Member.memberId;
   Submit.addEventListener('click', function (OnClick) {
      OnClick.preventDefault()
      SaveMember();
   });


   //testing ends  
}


function RemoveUser(Member) {
   const RemoveUrl = `https://localhost:7239/api/Member/${Member.memberId}`;
   const requestOptions = {
      method: 'delete',
      headers: {
         'Content-Type': 'application/json'
      },
   }
   fetch(RemoveUrl, requestOptions)
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
