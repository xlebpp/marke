import { createElement } from "react";

export function renderProfile(userData, container, isOwnProfile){
    const headerr = document.createElement("div");
    headerr.className="profile_header";

    const img = document.createElement("img");
    img.src = userData.photo ||"/Photos/ava.jpg";
    img.alt= userData.name;
    img.className="profile_photo";
    headerr.appendChild(img);

    const name = document.createElement("h2");
    name.textContent=userData.name || "Без имени";
    headerr.appendChild(name);

    container.appendChild(headerr);

    const mainContent = document.createElement("div");
    mainContent.className = "profile_content";

    if(userData.role==="User"){
        renderUserProfile(mainContent, userData, isOwnProfile);      
    }
    else if(userData.role==="Master"){
        renderMasterProfile(mainContent, userData, isOwnProfile);
    }
    else{
        mainContent.textContent="Неизвестный тип пользователя";
    }

    container.appendChild(mainContent);

    function renderUserProfile(container, userData, isOwnProfile){
        const title = document.createElement("h3");
        title.textContent = isOwnProfile ? "Мои индивидуальные заявки" : "Индивидуальные заявки";
        container.appendChild(title);

        if (userData.requests?.length>0){
            const list = document.createElement("div");
            list.className="request_list";

            userData.requests.forEach(req=>{
                const item = document.createElement("div");
                item.className="request_item";
                item.dataset.requestId=req.id;

                item.innerHTML = `
                        <p><strong>${req.title}</strong></p>
                        <p>${req.description || "Без описания"}</p>
                        <p><em>Статус: ${req.status}</em></p>`;

                list.appendChild(item);
            });

            container.appendChild(list);            
        }
        else{
            container.innerHTML+="<p>Заявок пока нет</p>";
        }
    }
    

    function renderMasterProfile(container, userData, isOwnProfile){
        const title = document.createElement("h3");
        title.textContent= isO
    }
const reqContainer = document.createElement("div");
        reqContainer.className="request_container";

        userData.requests.forEach(req=>{
            const reqCard = document.createElement("div");
            reqCard.className="request_card";
            reqCard.textContent=req.title||"Без названия";
            req.dataset.requestId=req.id;
            reqContainer.appendChild(reqCard);
        });