import { createElement } from "react";

export function renderProfile(userData, container){
    container.innerHTML="";

    const profileCard = document.createElement("div");
    profileCard.className=`profile_card ${userData.role}`;

    const img = document.createElement("img");
    img.src = userData.photo ||"/Photos/ava.jpg";
    img.alt= userData.name;
    img.className="profile_photo";
    profileCard.appendChild(img);

    const name = document.createElement("h2");
    name.textContent=userData.name || "Без имени";
    profileCard.appendChild(name);

    if(userData.role==="User" && userData.requests?.length){
        const reqContainer = document.createElement("div");
        reqContainer.className="request_container";

        userData.requests.forEach(req=>{
            const reqCard = document.createElement("div");
            reqCard.className="request_card";
            reqCard.textContent=req.title||"Без названия";
            req.dataset.requestId=req.id;
            reqContainer.appendChild(reqCard);
        });

        profileCard.appendChild(reqContainer);
    }

    if(userData.role==="Master"&&userData.products?.length){
        const prodContainer = document.createElement("div");
        prodContainer.className="product_conrainer";

        userData.products.forEach(prod=>{
            const prodCard=document.createElement("div");
            prodCard.className="product_card";
            prodCard.textContent=prod.name||"Без названия";
            prodCard.dataset.productId=prod.id;
            prodContainer.appendChild(prodCard);
        });

        profileCard.appendChild(prodContainer);
    }

    container.appendChild(profileCard);
}