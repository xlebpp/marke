export default class MasterCard{
    constructor(master, mode="prodCard"){
        this.master=master;
        this.mode=mode;        
    }

    render(){
        const card = document.createElement('div');
        card.className=`master_card_${this.mode}`;

        const img = document.createElement('img');
        img.src=this.master.photo ? `data:image/jpg;base64,${this.master.photo}`:"/Photos/ava.jpg";
        img.alt=this.master.name;
        img.className="master_photo";
        card.appendChild(img);

        const name = document.createElement('p');
        name.className="master_name";
        name.textContent=this.master.name;
        card.appendChild(name);

        if(this.mode==="prodCard"||this.mode==="search"){
            card.addEventListener("click", ()=>{
                window.location.href=`/master.html?id=${this.master.id}`;
            });
        }
        if(this.mode==="profile"){
            const rating = document.createElement('p');
            rating.className="master_rating";
            rating.textContent=this.master.rating&&this.master.rating!=0 ? `⭐${this.master.rating?.toFixed(1)}`:"У мастера пока нет оценок";
            card.appendChild(rating);
        }
        
        return card;
    }
    
}