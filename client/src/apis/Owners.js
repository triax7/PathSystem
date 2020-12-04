import axios from 'axios'

const api = axios.create({
    baseURL: 'https://localhost:5001/api/owneraccounts',
    timeout: 5000,
    withCredentials: true
})

async function register(credentials) {
    return api.post('register', credentials).then(res => res.data).catch(res => res.data)
}

async function login(credentials) {
    return api.post('login', credentials).then(res => res.data).catch(res => res.data)
}

async function emailExists(email) {
    return api.post('exists', {email}).then(res => res.data)
}

async function logout() {
    return api.post('logout')
}

async function current() {
    return api.get('current').then(res => res.data)
}

async function updateTokens() {
    return api.post('update-access-token').then(res => res.data)
}

api.interceptors.response.use((response) => {
    return response
}, async function (error) {
    const originalRequest = error.config
    if (error.response.status === 401 && !originalRequest._retry) {
        originalRequest._retry = true
        await updateTokens()
        return api(originalRequest)
    }
    return Promise.reject(error.response)
})

export {register, emailExists, login, logout, current}
