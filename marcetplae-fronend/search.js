import ProductCard from "./productCard.js";
import MasterCard from "./masterCard.js";
async function search(query) {

    try{
        const response = await fetch(`https://localhost:7104/api/search?search=${encodeURIComponent(query)}`);

        if(!response.ok){
            const errorText = await response.text();
            console.error("Ошибка поиска: ", errorText);
            document.getElementById("results").innerHTML=`<p>${errorText}</p>`;
            return;
        }

        const data = await response.json();

        renderResults(data.products, data.masters);
    }
    catch (err){
        console.error("Ошибка сети: ", err);
    }
    
}

function renderResults(products, masters){
    const resultsDiv = document.getElementById("results");
    const menuDiv = document.getElementById("menu");
    resultsDiv.style.display="flex";
    menuDiv.style.display="none";
    
    resultsDiv.innerHTML="";

    

    if((!products|| products.length===0)&&(!masters||masters.length===0)){
        resultsDiv.innerHTML="<p>Ничего не найдено</p>";
        return;
    }

    if(products&&products.length>0){
        const h3=document.createElement('h3');
        h3.textContent="Товары по вашему запросу";
        resultsDiv.appendChild(h3);
        
        products.forEach(element => {
            const card = new ProductCard(element, "list");
            resultsDiv.appendChild(card.render());
        });    
           
    }

    if(masters&&masters.length>0){
        const h3=document.createElement('h3');
        h3.textContent="Мастера по вашему запросу";
        resultsDiv.appendChild(h3);
        masters.forEach(m=>{
            const card = new MasterCard(m, "search");
            resultsDiv.appendChild(card.render())
        });
    }
}