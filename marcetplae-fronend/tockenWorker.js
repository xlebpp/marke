export function getTocken(){
    return localStorage.getItem("token");
    
}

export function parseJwt(token){
    try{
        const base64Url = tocken.split('.')[1];
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
    return decoded?.NameIdentifier || null;
}