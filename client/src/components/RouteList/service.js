import { makeAutoObservable } from 'mobx'
import { createRoute, getRoutes } from '../../apis/Routes'

export default class Service {
  state = {isEditing: false, initialInput: ''}

  constructor() {
    makeAutoObservable(this)
    this.fetchRoutes()
  }

  fetchRoutes = async () => {
    this.state.routes = await getRoutes()
  }

  handleEditing = () => {
    this.state.isEditing = true
  }

  handleSubmit = async (values) => {
    await createRoute(values.name)
    this.state.isEditing = false
    await this.fetchRoutes()
  }

  validateName = (name) => {
    if (name.length === 0) {
      return 'Route name can not be empty'
    }
  }
}
