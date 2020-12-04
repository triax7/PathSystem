import React from 'react'
import { observer } from 'mobx-react'
import Service from './service'
import { Typography } from '@material-ui/core'

const RouteList = observer(
  class extends React.Component {
    service

    constructor(props) {
      super(props)
      this.service = new Service()
    }

    render() {
      return <Typography>ROUTES</Typography>
    }
  }
)

export default RouteList
