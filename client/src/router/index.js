import Box from '@material-ui/core/Box'
import { Route, Router, Switch } from 'react-router'
import { history } from '../stores/RouterStore'
import React from 'react'
import RegistrationForm from '../components/RegistrationForm'
import LoginForm from '../components/LoginForm'
import RouteList from '../components/RouteList'
import Header from '../components/Header'
import MyRoute from '../components/Route'


export default function MyRouter() {
  return (
    <Box m={-1}>
      <Router history={history}>
        <Header/>
        <Switch>
          <Route exact path={'/register'} render={() => <RegistrationForm/>}/>
          <Route exact path={'/login'} render={() => <LoginForm/>}/>
          <Route exact path={'/routes'} render={() => <RouteList/>}/>
          <Route exact path={'/routes/:id'} render={(props) => <MyRoute {...props}/>}/>
        </Switch>
      </Router>
    </Box>
  )
}
