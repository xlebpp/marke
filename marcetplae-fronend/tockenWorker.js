export function getTocken(){
    return localStorage.getItem("token");
    
}

export function parseJwt(token){
    try{
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g,'+').replace(/_/g, '/');
        const jsonPayload = decodeURIComponent(
            atob(base64).split('').map(c=>
                '%' +('00' +c.charCodeAt(0).toString(16)).slice(-2)
            ).join('')
        );
        return JSON.parse(jsonPayload);
    }
    catch(err){
        console.error("ошибка при разборе токена", err);
        return null;
    }
}

export function getuserRoleFromTocken(){
    const token = getTocken();
    if(!token)return null;

    const decoded = parseJwt(token);
    return decoded?.role || null;
}

export function getUserIdFromTocken(){
    const token = getTocken();
    if(!token) return null;

    const decoded = parseJwt(token);
    console.log("Токен истекает:", new Date(decoded.exp * 1000));
    return decoded?.NameIdentifier || null;
    
}

