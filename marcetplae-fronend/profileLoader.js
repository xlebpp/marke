import { renderProfile } from "./profileRender.js";
import { getUserIdFromTocken, getuserRoleFromTocken } from "./tockenWorker.js";

export async function loadProfile(userId) {
    const container = document.getElementById('profile_container');
    container.innerHTML="Загрузка...";

    try{
        const response = await fetch(`api`)
    }
}