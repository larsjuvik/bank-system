window.login = async (loginDtoSerialized) => {
    let result = await fetch("/api/authentication/login", {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: loginDtoSerialized
    });
    return result.ok;
};

window.registerAccount = async (registerDtoSerialized) => {
    let result = await fetch("/api/authentication/register", {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: registerDtoSerialized
    });
    return result.ok;
};