import axios from 'axios'

const api = axios.create({
  baseURL: 'https://localhost:5001/api/pathpoints',
  timeout: 5000,
  withCredentials: true
})

async function createPoint(point) {
  console.log(point)
  return api.post('create', {
    routeId: point.routeId,
    name: point.name,
    longitude: point.longitude,
    latitude: point.latitude
  }).then(res => res.data)
}

export {createPoint}