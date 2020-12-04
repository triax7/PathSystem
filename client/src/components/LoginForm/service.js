import { makeAutoObservable } from 'mobx'
import { login } from '../../apis/Owners'

export default class Service {
  state = {}

  constructor() {
    makeAutoObservable(this)
    this.state.initialValues = {
      email: '',
      password: ''
    }
  }

  handleSubmit = async (values) => {
    let data = await login(values)
    if (data.errorMessage) {
      alert(data.errorMessage)
    }
  }

  validateEmail = async (value) => {
    if (!/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(value)) {
      return 'Invalid e-mail'
    }
  }
}
