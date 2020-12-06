import {makeAutoObservable} from 'mobx'

class GlobalStore {
  user = {}
  constructor() {
    makeAutoObservable(this)
  }
}

const globalStore = new GlobalStore()

export default globalStore
