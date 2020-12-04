import { makeAutoObservable } from 'mobx'
import { emailExists, register } from '../../apis/Owners'

export default class Service {
    state = {}

    constructor() {
        makeAutoObservable(this)
        this.state.initialValues = {
            name: '',
            email: '',
            password: ''
        }
    }

    handleSubmit = async (values) => {
        let data = await register(values)
        if(data.errorMessage) {
            alert(data.errorMessage)
        }
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
