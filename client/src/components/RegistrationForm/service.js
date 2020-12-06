import { makeAutoObservable } from 'mobx'
import { current, emailExists, register } from '../../apis/Owners'
import globalStore from '../../stores/GlobalStore'
import { history } from '../../stores/RouterStore'

export default class Service {
    state = {}

    constructor() {
        this.state.initialValues = {
            name: '',
            email: '',
            password: ''
        }
        makeAutoObservable(this)
    }

    handleSubmit = async (values) => {
        let data = await register(values)
        if(data.errorMessage) {
            alert(data.errorMessage)
        }
        globalStore.user = await current()
        history.push('/routes')
    }

    validateName = async (value) => {
        if(value.length < 4) {
            return 'Name is too short'
        }
        if (!/^[a-z0-9_]*$/.test(value)) {
            return 'Name should only consist of latin letters, digits or underscore'
        }
    }

    validateEmail = async (value) => {
        if (!/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/.test(value)) {
            return 'Invalid e-mail'
        }

        if(await emailExists(value))
            return 'Email is taken'
    }

    validatePassword = async (value) => {
        if(value.length < 4) {
            return 'Password is too short'
        }
    }
}
