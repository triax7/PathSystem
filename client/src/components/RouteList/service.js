import { makeAutoObservable } from 'mobx'

export default class Service {
  state = {}

  constructor() {
    makeAutoObservable(this)

  }

}
