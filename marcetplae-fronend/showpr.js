import ProductCard from './productCard.js';
export async function showpr() {

    try{
        const response = await fetch(`https://localhost:7104/api/products`);

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

function renderResults(products){
    const resultsDiv = document .getElementById("products-menu");

    resultsDiv.innerHTML="";

    if(!products||products.length===0){
        resultsDiv.innerHTML="<p>Ничего не найдено</p>";
        console.error("Почему-то нет продуктов");
    }

    if(products&&products.length>0){
        
        products.forEach(e => {
            const productCard = new ProductCard(e,"list");
            resultsDiv.appendChild(productCard.render());
            
        });
    }
}