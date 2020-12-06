import React from 'react'
import { observer } from 'mobx-react'
import Service from './service'
import Box from '@material-ui/core/Box'
import AppBar from '@material-ui/core/AppBar'
import Button from '@material-ui/core/Button'
import globalStore from '../../stores/GlobalStore'
import { history } from '../../stores/RouterStore'

const Header = observer(
  class extends React.Component {
    service

    constructor(props) {
      super(props)
      this.service = new Service()
    }

    render() {
      const isLoggedIn = !!globalStore.user.id
      const {handleLogout} = this.service
      return (
        <AppBar position={'sticky'}>
          <Box display={'flex'} justifyContent={'center'} alignItems={'center'}>
            {isLoggedIn ?
              <Box>
                <Button onClick={() => history.push('/routes')}>My routes</Button>
                <Button onClick={handleLogout}>Logout</Button>
              </Box> :
              <Box>
                <Button onClick={() => history.push('/register')}>Register</Button>
                <Button onClick={() => history.push('/login')}>Login</Button>
              </Box>}
          </Box>
        </AppBar>
      )
    }
  }
)

export default Header
