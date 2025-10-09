import ProductCard from "./productCard.js";

const params = new URLSearchParams(window.location.search);
const productId = params.get("id");

fetch(`https://localhost:7104/api/product?id=${productId}`)
  .then(r => {
    if (!r.ok) throw new Error("Ошибка загрузки товара");
    return r.json();
  })
  .then(product => {
    const detail = new ProductCard(product, "detail");
    document.getElementById("product_container").appendChild(detail.render());
  })
  .catch(err => console.error(err));