import { makeAutoObservable } from 'mobx'
import { getOptimizedRoute, getPoints } from '../../apis/Routes'
import { createPoint, deletePoint } from '../../apis/Points'

export default class Service {
  state = {isAddingPoint: false, inputName: '', selectedLat: 49.589463, selectedLng: 34.550992}

  constructor(routeId) {
    makeAutoObservable(this)
    this.state.routeId = routeId
    this.fetchPoints()
  }

  fetchPoints = async () => {
    this.state.pointsOptimized = null
    this.state.points = await getPoints(this.state.routeId)
    if (this.state.points.length > 0) {
      this.state.selectedLat = this.state.points[0].latitude
      this.state.selectedLng = this.state.points[0].longitude
    }
  }

  handlePointClick = (pointId) => {
    this.state.selectedPoint = this.state.points.find((point) => point.id === pointId)
  }

  handleAddingPoint = () => {
    this.state.isAddingPoint = true
  }

  handleNameChange = (event) => {
    this.state.inputName = event.target.value
  }

  handleMapClick = (event) => {
    this.state.selectedLat = event.lat
    this.state.selectedLng = event.lng
  }

  handleAdd = async () => {
    await createPoint({
      routeId: this.state.routeId,
      latitude: this.state.selectedLat,
      longitude: this.state.selectedLng,
      name: this.state.inputName
    })
    await this.fetchPoints()
    this.state.isAddingPoint = false
    this.state.inputName = ''
  }

  handleDelete = async (pointId) => {
    await deletePoint(pointId)
    if(this.state.selectedPoint?.id === pointId) this.state.selectedPoint = null
    await this.fetchPoints()
  }

  getOptimizedRoute = async () => {
    this.state.pointsOptimized = await getOptimizedRoute({
      routeId: this.state.routeId,
      startingPointId: Number(this.state.selectedPoint?.id ?? this.state.points[0].id)
    })
  }
}
