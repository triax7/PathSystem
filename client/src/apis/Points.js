import axios from 'axios'

const api = axios.create({
  baseURL: 'https://localhost:5001/api/pathpoints',
  timeout: 5000,
  withCredentials: true
})

async function createPoint(point) {
  return api.post('create', point).then(res => res.data)
}

async function deletePoint(pointId) {
  return api.post(`delete/${pointId}`)
}

export {createPoint, deletePoint}
