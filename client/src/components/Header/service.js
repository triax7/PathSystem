import { makeAutoObservable } from 'mobx'
import { current, logout } from '../../apis/Owners'
import { history } from '../../stores/RouterStore'
import globalStore from '../../stores/GlobalStore'

export default class Service {
  state = {}

  constructor() {
    makeAutoObservable(this)
    this.loadUser()
  }

  loadUser = async () => {
    let user = await current()
    console.log(user)
    if (!user) {
      history.push('/login')
    } else {
      globalStore.user = user
    }
  }

  handleLogout = async () => {
    await logout()
    globalStore.user = {}
    history.push('/login')
  }
}
