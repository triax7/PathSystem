import { observer } from 'mobx-react'
import React from 'react'
import { IconButton, Typography } from '@material-ui/core'
import Service from './service'
import Box from '@material-ui/core/Box'
import Grid from '@material-ui/core/Grid'
import Paper from '@material-ui/core/Paper'
import TableContainer from '@material-ui/core/TableContainer'
import Table from '@material-ui/core/Table'
import TableHead from '@material-ui/core/TableHead'
import TableRow from '@material-ui/core/TableRow'
import TableCell from '@material-ui/core/TableCell'
import TableBody from '@material-ui/core/TableBody'
import { Add, AddLocation, Clear, Done, LocationOn } from '@material-ui/icons'
import GoogleMapReact from 'google-map-react'
import TextField from '@material-ui/core/TextField'
import Button from '@material-ui/core/Button'

const OutPaper = (props) => {
  return <Paper {...props} variant={'outlined'}>{props.children}</Paper>
}

const MyMarker = ({pointId, handlePointClick, selected, label}) => {
  const color = selected ? 'error' : 'inherit'

  return <Box><LocationOn
    style={{transform: 'translate(-50%, -100%)'}}
    color={color}
    onClick={() => handlePointClick(pointId)}/>
    <Typography style={{transform: 'translate(-100%, -120%)'}}>{label}</Typography>
  </Box>
}

const AddingMarker = () => {
  return <AddLocation style={{color: '#4e8d2b', transform: 'translate(-50%, -100%)'}}/>
}

const Route = observer(
  class extends React.Component {
    service

    constructor(props) {
      super(props)
      this.service = new Service(Number(props.match.params.id))
    }

    render() {
      let {points} = this.service.state
      const {
        selectedPoint,
        isAddingPoint,
        inputName,
        selectedLat,
        selectedLng,
        pointsOptimized
      } = this.service.state
      const {
        handlePointClick,
        handleAddingPoint,
        handleAdd,
        handleNameChange,
        handleMapClick,
        handleDelete,
        getOptimizedRoute
      } = this.service
      let center = {lat: 49.589463, lng: 34.550992}
      if (points === undefined) return <Box>loading...</Box>
      if (points.length > 0) {
        center = {
          lat: this.service.state.points[0].latitude,
          lng: this.service.state.points[0].longitude
        }
      }
      if (pointsOptimized) points = pointsOptimized
      return (
        <Box mt={6}>
          <Grid container justify={'center'}>
            <Grid item xs={6}>
              <Paper variant={'outlined'}>
                <Box display={'flex'} flexDirection={'column'} alignItems={'stretch'}>
                  <Box p={2}>
                    {points.length === 0 && !isAddingPoint ?
                      <Box display={'flex'} justifyContent={'center'}>
                        <Typography>This route has no points</Typography>
                      </Box> :
                      <TableContainer component={OutPaper}>
                        <Table size={'small'}>
                          <TableHead>
                            <TableRow>
                              <TableCell>Name</TableCell>
                              <TableCell align="right">Latitude</TableCell>
                              <TableCell align="right">Longitude</TableCell>
                              <TableCell align="right"/>
                            </TableRow>
                          </TableHead>
                          <TableBody>
                            {points.map(point =>
                              <TableRow key={point.id}
                                        style={point.id === selectedPoint?.id ? {backgroundColor: '#e0e0e0'} : {}}>
                                <TableCell>
                                  {point.name}
                                </TableCell>
                                <TableCell align="right">{point.latitude}</TableCell>
                                <TableCell align="right">{point.longitude}</TableCell>
                                <TableCell align="right">
                                  <IconButton size={'small'} onClick={() => handleDelete(point.id)}>
                                    <Clear/>
                                  </IconButton>
                                </TableCell>
                              </TableRow>)
                            }
                            {isAddingPoint &&
                            <TableRow>
                              <TableCell>
                                <TextField value={inputName} onChange={handleNameChange}/>
                              </TableCell>
                              <TableCell align="right">{selectedLat}</TableCell>
                              <TableCell align="right">{selectedLng}</TableCell>
                              <TableCell align="right">
                                <IconButton size={'small'} onClick={handleAdd}>
                                  <Done/>
                                </IconButton>
                              </TableCell>
                            </TableRow>
                            }
                          </TableBody>
                        </Table>
                      </TableContainer>
                    }
                    {!isAddingPoint &&
                    <Box display={'flex'} justifyContent={'center'} p={1}>
                      <IconButton onClick={handleAddingPoint} size={'small'}>
                        <Add/>
                      </IconButton>
                    </Box>}
                    {!isAddingPoint && points.length > 0 &&
                    <Box display={'flex'} justifyContent={'center'}>
                      <Button variant={'outlined'} onClick={getOptimizedRoute}>Optimize</Button>
                    </Box>}
                  </Box>
                </Box>
                {/*{pointsOptimized &&*/}
                {/*<Box pb={2} display={'flex'} justifyContent={'center'}>*/}
                {/*  <Typography>{pointsOptimized.map(point => point.name).join(' => ')}</Typography>*/}
                {/*</Box>}*/}
              </Paper>
              <Box mt={5} width={1} height={'50vh'}>
                <GoogleMapReact
                  bootstrapURLKeys={{key: process.env.REACT_APP_GOOGLE_API_KEY}}
                  defaultCenter={center}
                  defaultZoom={15}
                  onClick={handleMapClick}
                >
                  {points.map((point, index) =>
                    <MyMarker key={point.id}
                              lat={point.latitude}
                              lng={point.longitude}
                              pointId={point.id}
                              handlePointClick={handlePointClick}
                              selected={point.id === selectedPoint?.id}
                              label={pointsOptimized ? index + 1 : ''}
                    />)}
                  {isAddingPoint && <AddingMarker lat={selectedLat} lng={selectedLng}/>}
                </GoogleMapReact>
              </Box>
            </Grid>
          </Grid>
        </Box>

      )
    }
  }
)

export default Route
