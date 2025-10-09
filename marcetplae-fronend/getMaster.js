import MasterCard from "./masterCard.js";
export async function getMaster() {
    try{
        const response =await fetch(`https://localhost:7104/api/masters`);

        if(!response.ok){
            const errorText=await response.text();
            console.error("Ошибка", errorText);
            document.getElementById("menu").innerHTML=`<p>${errorText}</p>`;
            return;
        }
        const data= await response.json();
        renderResults(data);
    }
    catch (err){
        console.error("Ошибка тут", err);
    }
    
}

function renderResults(master){
    const resultsDiv=document.getElementById("masters-menu");

    resultsDiv.innerHTML="";
    if(!master||master.length===0){
        resultsDiv.innerHTML="<p>Мастера не найдены</p>";
        console.error("Произошла ошибка");
    }

    if(master&&master.length>0){
        master.forEach(e=>{
            const masterCard= new MasterCard(e,"search");
            resultsDiv.appendChild(masterCard.render());
        })
    }
}

/*для вывода мастеров на начальной странице - типа вот например такие мастера, может вас заинтересует */