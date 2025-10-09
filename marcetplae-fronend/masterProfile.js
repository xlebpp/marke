import MasterCard from "./masterCard.js";
import { getUserIdFromTocken } from "./tockenWorker.js";
const params = new URLSearchParams(window.location.search);
const masterId = params.get("id");

const currUserId= getUserIdFromTocken();

if(masterId==currUserId){
    loadMyProfile(); /*реализацию метода отдельно */
}
else{
    loadMasterProfile(masterId);
}

async function loadMasterProfile(masterId) {
    

    const m = params.get("NameIdentifier");
    console.log("TOCKEN",masterId, m);

    const response = await fetch(`/api/master?id=${masterId}`);
    if(!response.ok){
        console.error("Мастер не найден");
        return;
    }

    const masterData = await response.json();
    const profileContainer = document.getElementById("master_profile");
    const card = new MasterCard(masterData, "profile");
    profileContainer.appendChild(card.render());
}

document.addEventListener("DOMContentLoaded",()=> loadMasterProfile(masterId));