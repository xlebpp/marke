import MasterCard from "./masterCard.js";
export default class ProductCard{
    constructor(product, mode ="list"){
        this.product=product;
        this.mode=mode;
        this.currentIndex=0;
        this.preloaded = [];
        this.images=Array.isArray(product.images) ? product.images :[];
        this.images.forEach(base64 => {
            const im =new Image();
            im.src=`data:image/jpg;base64,${base64}`;
            this.preloaded.push(im);
        });
    }

    render(){
        const card = document.createElement('div');
        card.className=this.mode==="list" ? 'product_card_list' : 'product_card_detail';

        const carousel=document.createElement('div');
        carousel.className='carousel';

        const img = document.createElement('img');
        img.className='product_img';
        img.alt='Изображение товара';
        img.src=this.images.length>0
            ? `data:image/jpg;base64,${this.images[0]}`:'/Photos/ava.jpg';

        carousel.appendChild(img);

        if(this.images.length>1){
            const prevBtn=document.createElement('button');
            prevBtn.className='prev';
            prevBtn.textContent='◀';
            prevBtn.addEventListener('click', (event)=>{
                event.stopPropagation();
                this.showPrev(img);
            });
            
            const nextBtn=document.createElement('button');
            nextBtn.className='next';
            nextBtn.textContent='▶';
            nextBtn.addEventListener('click', (event)=>{
                    event.stopPropagation();
                    this.showNext(img);
            });
            

            carousel.appendChild(prevBtn);
            carousel.appendChild(nextBtn);            
        }

        card.appendChild(carousel);

        const title = document.createElement('h4');
        title.className='product_name';
        title.textContent=this.product.name||'Без названия';
        card.appendChild(title);

        const price = document.createElement('p');
        price.className='product_price';
        price.textContent=this.product.price;
        card.appendChild(price);

        if(this.product.master){
            const masterCard = new MasterCard(this.product.master, "prodCard");
            card.appendChild(masterCard.render());
        }

        const rating=document.createElement('p');
        rating.className='product_rating';
        rating.textContent= this.product.rating&&this.product.rating!=0 ? `⭐${this.product.rating?.toFixed(1)}`:"Нет оценок";
        card.appendChild(rating);

        if(this.mode==="detail"&&this.product.description){
            const descr=document.createElement('p');
            descr.className='product_description';
            descr.textContent=this.product.description;
            card.appendChild(descr);
        }

        if(this.mode==="list"){
            card.addEventListener('click',()=>{
                window.location.href=`/product.html?id=${this.product.productId}`;
            });
        }
        return card;
    }

    showPrev(img){
        this.currentIndex=(this.currentIndex-1+this.images.length)%this.images.length;
        img.src=this.preloaded[this.currentIndex].src;
    }

    showNext(img){
        this.currentIndex=(this.currentIndex+1)%this.images.length;
        img.src=this.preloaded[this.currentIndex].src;
    }
}