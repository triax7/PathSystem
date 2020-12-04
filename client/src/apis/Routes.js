import axios from 'axios'

const api = axios.create({
  baseURL: 'https://localhost:5001/api/routes',
  timeout: 5000,
  withCredentials: true
})

async function createRoute(name) {
  return api.post('create', {name}).then(res => res.data)
}

async function getRoutes() {
  return api.get('').then(res => res.data)
}

async function getRoute(id) {
  return api.get(`${id}`).then(res => res.data)
}

async function getPoints(routeId) {
  return api.get(`get-points/${routeId}`).then(res => res.data)
}

async function getOptimizedRoute(values) {
  return api.post('optimize', {
    routeId: values.routeId,
    startingPointId: values.startingPointId
  }).then(res => res.data)
}

export { createRoute, getRoutes, getRoute, getPoints, getOptimizedRoute }
