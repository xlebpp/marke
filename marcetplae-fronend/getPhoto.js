export async function loadAvatar(elementId="avatar"){
                try{
                    const token = localStorage.getItem("token");
                   
                    const response = await fetch ("https://localhost:7104/api/user/avatar",{
                        method:"GET",
                        headers:{
                            "Authorization": "Bearer " + token
                        }
                    });
                    console.log(token);
                    if(response.ok){
                        const data = await response.json();
                        console.log("full response:", data);
                        console.log("photo field:", data.value.photo);
                        if(data.value.photo){
                            document.getElementById("avatar").src=`data:image/png;base64,${data.value.photo}`;
                           
                        }
                        else{
                            document.getElementById("avatar").src = "/Photos/ava.jpg"
                        }
                    }
                    else{
                        console.error("Ошибка при получении фото");
                    }
                }
                catch(err){
                    console.error("Ошибка запроса", err);
                }
            };