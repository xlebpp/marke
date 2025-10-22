import { renderProfile } from "./profileRender.js";
import { getUserIdFromTocken, getuserRoleFromTocken } from "./tockenWorker.js";


export async function loadProfile() {
    
        const params = new URLSearchParams(window.location.search);
        const openedProfileId = params.get("id");
        const currentUserId = getUserIdFromTocken();

        let url;
        let isOwnProfile = false;

        if(!openedProfileId || openedProfileId===currentUserId ){
            url = "https://localhost:7104/api/profile/my";
            isOwnProfile=true;
        }
        else{
            url=`https://localhost:7104/api/profile/${openedProfileId}`;
        }
        
        const token = localStorage.getItem("token");

        try{
            const response=await fetch (url);

            if (!response.ok){
                console.error("Ошибка загрузки профиля:", response.statusText);
                return;
            }

            const userData = await response.json();
            const container = document.getElementById("profile_container");
            container.innerHTML="";
            
            renderProfile(userData,container,isOwnProfile);
        }

        catch (error){
            console.error("Ошибка при получении профиля", error);
        }
}

document.addEventListener("DOMContentLoaded", loadProfile);
