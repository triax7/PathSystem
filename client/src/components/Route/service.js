import { makeAutoObservable } from 'mobx'
import { getPoints } from '../../apis/Routes'
import { createPoint } from '../../apis/Points'

export default class Service {
  state = {isAddingPoint: false, inputName: '', selectedLat: 49.589463, selectedLng: 34.550992}

  constructor(routeId) {
    makeAutoObservable(this)
    this.state.routeId = routeId
    this.fetchPoints()
  }

  fetchPoints = async () => {
    console.log(await getPoints(this.state.routeId))
    this.state.points = await getPoints(this.state.routeId)
    if (this.state.points[0]) {
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
}
