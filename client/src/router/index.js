import Box from '@material-ui/core/Box'
import { Route, Router, Switch } from 'react-router'
import { history } from '../stores/RouterStore'
import React from 'react'
import RegistrationForm from '../components/RegistrationForm'
import LoginForm from '../components/LoginForm'
import RouteList from '../components/RouteList'


export default function () {

  return (
    <Box m={-1}>
      <Router history={history}>
        <Switch>
          <Route exact path={'/register'} render={() => <RegistrationForm/>}/>
          <Route exact path={'/login'} render={() => <LoginForm/>}/>
          <Route exact path={'/routes'} render={() => <RouteList/>}/>
        </Switch>
      </Router>
    </Box>
  )
}
